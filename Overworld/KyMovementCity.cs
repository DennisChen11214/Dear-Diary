using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KyMovementCity : MonoBehaviour
{
    Rigidbody2D rb;

    float horizontal;
    float vertical;
    Vector2 directionDetect; //The player's movement input through the controller
    public float speed = 5.0f;
    public static int directionFacing;

    PlayerControls controls;
    bool itemInteract;
    GameObject interactObject;
    Animator animator;
    //Nova, Simon, and Nikko when they're following him
    [SerializeField] GameObject nova;
    [SerializeField] GameObject simon;
    [SerializeField] GameObject nikko;
    GameObject follower;

    private void Awake() {
        controls = new PlayerControls();
        controls.City.Move.performed += ctx => directionDetect = ctx.ReadValue<Vector2>();
        controls.City.Move.canceled += ctx => directionDetect = Vector2.zero;
        controls.City.PickUp.performed += ctx => Interact();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        follower = null;
        //Create Nova, Simon, or Nikko in the scene if they're supposed to be following him
        if(GameManager.Instance.day == 1 && DecisionManager.Instance.beforeClassDay1 && !DecisionManager.Instance.puzzleDoneDay1){
            follower = Instantiate(nova,transform.position,Quaternion.identity);
        }
        else if(GameManager.Instance.day == 2 && DecisionManager.Instance.withSimonDay2 && !DecisionManager.Instance.puzzleDoneDay2){
            follower = Instantiate(simon,transform.position,Quaternion.identity);
        }
        else if(GameManager.Instance.day == 3 && DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.puzzleDoneDay3){
            follower = Instantiate(nikko,transform.position,Quaternion.identity);
        }
        //Gets the sorting order of the character following him, so we can change the sprite on top to be correct
        if(follower){
            follower.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        }
        //Play a different idle animation depending on what direction Ky is facing at the beginning, follower also does the same
        switch(directionFacing){
            case -2:
                animator.Play("IdleFront");
                if(follower){
                    follower.GetComponent<Animator>().Play("IdleFront");
                    follower.transform.position = transform.position + new Vector3(0,1,1);
                }
                break;
            case -1:
                animator.Play("IdleSide");
                GetComponent<SpriteRenderer>().flipX = true;
                if(follower){
                    follower.GetComponent<Animator>().Play("IdleSide");
                    follower.transform.position = transform.position + new Vector3(1,0,0);
                    follower.GetComponent<SpriteRenderer>().flipX = true;
                }
                break;
            case 1:
                animator.Play("IdleSide");
                GetComponent<SpriteRenderer>().flipX = false;
                if(follower){
                    follower.GetComponent<Animator>().Play("IdleSide");
                    follower.transform.position = transform.position + new Vector3(-1,0,0);
                    follower.GetComponent<SpriteRenderer>().flipX = false;
                }
                break;
            case 2:
                animator.Play("IdleBack");
                if(follower){
                    follower.GetComponent<Animator>().Play("IdleBack");
                    follower.transform.position = transform.position + new Vector3(0,-1,-1);
                }
                break;
        }
        //Follower sets the target to Ky
        if(follower)
            follower.GetComponent<FollowKy>().SetTarget(gameObject);
    }

    public static void Face(int direction){
        directionFacing = direction;
    }

    void FixedUpdate()
    {
        int pastDir = directionFacing;
        //Check if we're idle
        if(Mathf.Abs(rb.velocity.x) < 0.1f && Mathf.Abs(rb.velocity.y) < 0.1f){
            animator.SetBool("idle", true);
            animator.SetBool("xMovement",false);
            animator.SetBool("yMoving",false);
            animator.SetFloat("yMovement",0);
        }
        else{
            animator.SetBool("idle", false);
        }
        //Check if we're allowed to move
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE ||
                GameManager.Instance.fadingIn || GameManager.Instance.fadingOut){
            rb.velocity = Vector3.zero;
            return;
        }
        //Change the animator variables depending on what direction Ky's moving
        if(Mathf.Abs(rb.velocity.x) > 0.1f){
            animator.SetBool("xMovement",true);
            if(rb.velocity.x > 0.1f){
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if(rb.velocity.x < -0.1f){
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else{
            animator.SetBool("xMovement",false);
        }
        if(Mathf.Abs(rb.velocity.y) > 0.1f){
            animator.SetBool("yMoving", true);
        }
        else{
            animator.SetBool("yMoving",false);
        }
        animator.SetFloat("yMovement", rb.velocity.y);
        //Calculate Ky's velocity of the direction he's moving in
        if(Mathf.Abs(directionDetect.x) > 0){
            if(Mathf.Abs(directionDetect.y) > 0){
                if(directionDetect.y > 0){
                    directionFacing = 2;
                }
                else{
                    directionFacing = -2;
                }
                rb.velocity = new Vector2(0, directionDetect.y) * speed;
            }
            else{
                if(directionDetect.x > 0){
                    directionFacing = 1;
                }
                else{
                    directionFacing = -1;
                }
                rb.velocity = new Vector2(directionDetect.x, 0) * speed;
            }
        }
        else if(Mathf.Abs(directionDetect.y) > 0){
            if(Mathf.Abs(directionDetect.x) > 0){
                if(directionDetect.x > 0){
                    directionFacing = 1;
                }
                else{
                    directionFacing = -1;
                }
                rb.velocity = new Vector2(directionDetect.x, 0) * speed;
            }
            else{
                if(directionDetect.y > 0){
                    directionFacing = 2;
                }
                else{
                    directionFacing = -2;
                }
                rb.velocity = new Vector2(0, directionDetect.y) * speed;
            }
        }
        else{
            rb.velocity = Vector2.zero;
        }
    }

    //Ky interacts with an item to pick it up
    void Interact(){
        if(GameManager.Instance.CurrentGameState != GameManager.GameState.TOWN){
            return;
        }
        if(itemInteract){
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.keyItem);
            //If it's the diary, add it to the inventory
            if(interactObject.GetComponent<Item>().itemInformation.itemType != ItemInformation.ItemType.Diary)
                PlayerInventory.AddItem(interactObject.GetComponent<Item>().itemInformation);
            //Start a dialogue as well
            if(interactObject.GetComponent<Item>().itemInformation.itemType == ItemInformation.ItemType.Diary){
                PickUpDiary();
            }
            //If we picked up the ID card, start the corresponding dialogue
            else if(interactObject.GetComponent<Item>().itemInformation.itemType == ItemInformation.ItemType.IDCard){
                DecisionManager.Instance.hasIdCard = true;
                DialogueManager.Instance.StartConversation("GetIDDialogue");
            }
            Destroy(interactObject);
            itemInteract = false;
        }
    }

    //Start the dialogue to the corresponding day and add the diary page to the diary
    void PickUpDiary(){
        switch(GameManager.Instance.day){
            case 1:
                PlayerInventory.AddItem(interactObject.GetComponent<Item>().itemInformation);
                DialogueManager.Instance.StartConversation("diaryDay1Dorm");
                DecisionManager.Instance.diaryCheckedDay1 = true;
                Diary.AddPage(interactObject.GetComponent<Item>().itemInformation);
                break;
            case 2:
                DialogueManager.Instance.StartConversation("KyDormDiaryDay2");
                DecisionManager.Instance.diaryCheckedDay2 = true;
                Diary.AddPage(interactObject.GetComponent<Item>().itemInformation);
                break;
            case 3:
                DialogueManager.Instance.StartConversation("KyDormDiaryPickUp");
                DecisionManager.Instance.diaryCheckedDay3 = true;
                Diary.AddPage(interactObject.GetComponent<Item>().itemInformation);
                break;
            case 4:
                if(!DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
                    DialogueManager.Instance.StartConversation("KyDiaryDay4DiaryRoute");
                }
                else{
                    DialogueManager.Instance.StartConversation("KyDormDiaryDay4NeutralKy");
                }
                DecisionManager.Instance.diaryCheckedDay4 = true;
                Diary.AddPage(interactObject.GetComponent<Item>().itemInformation);
                break;
        }
    }

    //If we're on top of an item, indicate that we are
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Item"){
            itemInteract = true;
            interactObject = other.gameObject;
        }
    }

    //If we're no longer on top of an item, indicate that we aren't
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Item"){
            itemInteract = false;
            interactObject = null;
        }
    }

    private void OnEnable() {
        controls.City.Enable();
    }

    private void OnDisable() {
        controls.City.Disable();
    }
}
