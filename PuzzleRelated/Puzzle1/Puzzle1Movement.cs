using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Movement : PuzzleMovement
{
    #region Public Variables
    #endregion
    #region Serialized Fields
    [SerializeField] bool personBehind; //True if this character is in the back
    [SerializeField] Power ability; //Disabled if this character is in the back
    [SerializeField] GameObject other;
    #endregion

    #region Private Variables
    static List<Vector3> pastPositions = new List<Vector3>(); //List of positions for the person in the back to use to move
    static Vector3[] swapPositions = new Vector3[2]; //Position of the player in the front and in the back
    static float pastDirection = -1; // For the person in behind to have a proper animation
    float tempPastDirection = -3;

    #endregion

    protected override void Awake() {
        base.Awake();
        controls.Gameplay.SwapChar.performed += ctx => SwapChar();
        //Initialize the swapPositions and pastPositions with the initial positions of the characters
        if(personBehind){
            swapPositions[1] = transform.position;
            ability.enabled = false;
        }
        else{
            pastPositions.Clear();
            swapPositions[0] = transform.position;
            pastPositions.Add(transform.position);
        }
    }

    protected override void Start ()
    {
        base.Start();
        //Change the direction of the person in the back relative to the person in the front
        if(personBehind){
            if(swapPositions[0].x - swapPositions[1].x > 1){
                tempPastDirection = 1;
                pastDirection = 1;
            }
            else if(swapPositions[0].x - swapPositions[1].x < -1){
                tempPastDirection = -1;
                pastDirection = -1;
            }
            else if(swapPositions[0].y - swapPositions[1].y > 1){
                tempPastDirection = 2;
                pastDirection = 2;
            }
            else{
                tempPastDirection = -2;
                pastDirection = -2;
            }
        }
    }

    void Update()
    {
        //Don't have any movement animations in dialogue, scene changes, or when viewing diary pages
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE || GameManager.Instance.fadingIn ||
           GameManager.Instance.CurrentGameState == GameManager.GameState.INVENTORY){
            animator.SetBool("idle", true);
            animator.SetFloat("yMovement", 0);
            animator.SetBool("yMoving", false);
            animator.SetBool("xMovement", false);
            return;
        }
        CalcDirection();
        //Makes it so that the character doesn't play the wrong direction's animation while moving
        if(moving && !animSameDir){
            animSameDir = true;
            animator.SetBool("idle", false);
            //Depending on the direction the character is moving, set the animation variables accordingly
            if((direction == 1 && !personBehind) || (pastDirection == 1 && personBehind)){
                animator.SetFloat("yMovement", 0);
                animator.SetBool("yMoving", false);
                animator.SetBool("xMovement", true);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if((direction == -1 && !personBehind) || (pastDirection == -1 && personBehind)){
                animator.SetFloat("yMovement", 0);
                animator.SetBool("yMoving", false);
                animator.SetBool("xMovement", true);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if((direction == 2 && !personBehind) || (pastDirection == 2 && personBehind)){
                animator.SetFloat("yMovement", 1);
                animator.SetBool("yMoving", true);
                animator.SetBool("xMovement", false);
            }
            else if((direction == -2 && !personBehind) || (pastDirection == -2 && personBehind)){
                animator.SetFloat("yMovement", -1);
                animator.SetBool("yMoving", true);
                animator.SetBool("xMovement", false);
            }
        }
        //Idle State
        else if(!moving){
            animSameDir = false;
            animator.SetBool("idle", true);
            animator.SetFloat("yMovement", 0);
            animator.SetBool("yMoving", false);
            animator.SetBool("xMovement", false);
        }
        RaycastHit2D hit2D = new RaycastHit2D();
        //Person behind and in the front checks if there's anything in the way
        if(personBehind)
            hit2D = Physics2D.Raycast(swapPositions[0], directionMoving, stepDistance, 1 << 9);
        else if(!personBehind){
            hit2D = Physics2D.Raycast(transform.position, directionMoving, stepDistance, 1 << 9);
        }
        if(!moving && direction != 0){
            tempPastDirection = direction;
        }
        //Disregard special tiles
        if(hit2D && hit2D.collider.tag == "SpecialTile")
            hit2D = new RaycastHit2D();
        //Move if not currently moving and there's nothing in the way
        if(sameDir && !hit2D && !moving){
            moving = true;
            if(!personBehind){
                newPos = transform.position + (Vector3)directionMoving * stepDistance;
            }
            else{
                newPos = pastPositions[0];
                pastPositions.RemoveAt(0);
            }
            StartCoroutine(Move(null, oldPos, newPos));
        }
        else if(sameDir && hit2D){
            sameDir = false;
        }
        if(direction == 0){
            sameDir = false;
        }
    }

    //Figure out the direction that the character is facing based on input
    protected void CalcDirection(){
        horizontal = directionDetect.x;
        vertical = directionDetect.y;
        //Gets the old direction that the player is facing
        float oldDir = direction;
        //Set the new direction the player is facing
        if(direction == 1 || direction == -1){
            if(horizontal == 0 && vertical != 0)
                direction = 2 * vertical;
            else if(horizontal != 0)
                direction = horizontal;
            else
                direction = 0;
        }
        else{
            if(vertical == 0 && horizontal != 0)
                direction = horizontal;
            else if(vertical != 0)
                direction = 2 * vertical;
            else
                direction = 0;
        }
        //If the player is not idle
        if(direction != 0){
            DirectionFacing = direction;
            //If there's something in the way, still have the character in the front face that way and remain idle
            if(!moving && !personBehind){
                animator.SetBool("idle", true);
                if(direction == 1 && !personBehind){
                    animator.Play("IdleSide");
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else if(direction == -1 && !personBehind){
                    animator.Play("IdleSide");
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if(direction == 2 && !personBehind){
                    animator.Play("IdleBack");
                }
                else if(direction == -2 && !personBehind){
                    animator.Play("IdleFront");
                }
            }
        }
        if(!sameDir){
            sameDir = oldDir == direction && oldDir != 0;
            if(DirectionFacing == 1 || DirectionFacing == -1)
                directionMoving = new Vector2(horizontal, 0);
            else
                directionMoving = new Vector2(0, vertical);
        }
    }

    //Move a set distance based on the direction facing
    IEnumerator Move(GameObject movingObject, Vector3 oldP, Vector3 newP)
    {
        float timeElapsed = 0;
        //Move the character stepDistance away
        while (timeElapsed < 1)
        {
            transform.position = Vector3.Lerp(oldPos, newPos, timeElapsed);
            timeElapsed += Time.deltaTime * 2;
            yield return null;
        }
        transform.position = Vector3.Lerp(oldPos, newPos, 1);
        sameDir = false;
        oldPos = transform.position;
        moving = false;
        if(personBehind){
            swapPositions[1] = transform.position;
        }
        else{
            swapPositions[0] = transform.position;
            pastPositions.Add(transform.position);
            pastDirection = tempPastDirection;
        }
    }

    //Swap the position of both characters and disable the ability of the one in the back
    void SwapChar(){
        if(moving || GameManager.Instance.CurrentGameState != GameManager.GameState.PUZZLE || !other.activeSelf)
            return;
        //Both characters swap positions and toggle their abilities
        if(personBehind){
            transform.position = swapPositions[0];
            oldPos = transform.position;
            personBehind = false;
            ability.enabled = true;
        }
        else{
            transform.position = swapPositions[1];
            oldPos = transform.position;
            personBehind = true;
            ability.enabled = false;
        }
        //Changes the direction that the characters are facing based on the direction of the characters before the swap
        if(other.GetComponent<PuzzleMovement>().DirectionFacing == 1){
            animator.Play("IdleSide");
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(other.GetComponent<PuzzleMovement>().DirectionFacing == -1){
            animator.Play("IdleSide");
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(other.GetComponent<PuzzleMovement>().DirectionFacing == 2){
            animator.Play("IdleBack");
        }
        else if(other.GetComponent<PuzzleMovement>().DirectionFacing == -2){
            animator.Play("IdleFront");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //If the player made it to the exit
        if(other.gameObject.tag == "Exit"){
            //Add a new page to the diary on day 1
            if(GameManager.Instance.day == 1){
                DialogueManager.Instance.StartConversation("Day1PuzzleEnd");
                ItemInformation diaryEndPage = new ItemInformation();
                diaryEndPage.itemType = ItemInformation.ItemType.Diary;
                diaryEndPage.day = 1;
                diaryEndPage.end = true;
                Diary.AddPage(diaryEndPage);
            }
            //Day 4 - start dialogues based on if Nikko/Simon was with us on previous days
            else if(GameManager.Instance.day == 4 && HasKey(PlayerInventory.GetInventory())){
                RemoveKey(PlayerInventory.GetInventory());
                if(!DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2)
                    DialogueManager.Instance.StartConversation("Day4Puzzle1NeutralSimonEnd");
                else if(DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2)
                    DialogueManager.Instance.StartConversation("Day4Puzzle1NeutralNikkoEnd");
                else if(DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2)
                    DialogueManager.Instance.StartConversation("Day4Puzzle1KyEnd");
                else{
                    DialogueManager.Instance.puzzleDialogueDone = false;
                    GameManager.Instance.LoadScene("Day4SecondPuzzle", true, true);
                }
            }
        }    
    }

}
