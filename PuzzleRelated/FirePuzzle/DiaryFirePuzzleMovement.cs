using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiaryFirePuzzleMovement : PuzzleMovement
{
    #region Public Variables
    #endregion
    #region Serialized Fields
    //Fire object that gets instantiated behind the character every time he moves
    [SerializeField] GameObject fire;
    #endregion

    #region Private Variables
    //Controls whether no fire is spawned behind the character
    bool noFire = false;
    #endregion

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
            if(direction == 1){
                animator.SetFloat("yMovement", 0);
                animator.SetBool("yMoving", false);
                animator.SetBool("xMovement", true);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if(direction == -1){
                animator.SetFloat("yMovement", 0);
                animator.SetBool("yMoving", false);
                animator.SetBool("xMovement", true);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(direction == 2){
                animator.SetFloat("yMovement", 1);
                animator.SetBool("yMoving", true);
                animator.SetBool("xMovement", false);
            }
            else if(direction == -2){
                animator.SetFloat("yMovement", -1);
                animator.SetBool("yMoving", true);
                animator.SetBool("xMovement", false);
            }
        }
        //Idle state
        else if(!moving){
            animSameDir = false;
            animator.SetBool("idle", true);
            animator.SetFloat("yMovement", 0);
            animator.SetBool("yMoving", false);
            animator.SetBool("xMovement", false);
        }
        //Check if there's something in the character's way
        RaycastHit2D hit2D = new RaycastHit2D();;
        hit2D = Physics2D.Raycast(transform.position, directionMoving, stepDistance, 1 << 9);
        //Disregard special tiles
        if(hit2D && hit2D.collider.tag == "SpecialTile")
            hit2D = new RaycastHit2D();
        //Move if not currently moving and there's nothing in the way
        if(sameDir && !hit2D && !moving){
            moving = true;
            newPos = transform.position + (Vector3)directionMoving * stepDistance;
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
        //Gets the old direction that the player was facing
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
            //If there's something in the way, still have the character face that way and remain idle
            if(!moving){
                animator.SetBool("idle", true);
                if(direction == 1){
                    animator.Play("IdleSide");
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else if(direction == -1){
                    animator.Play("IdleSide");
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if(direction == 2){
                    animator.Play("IdleBack");
                }
                else if(direction == -2){
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
        //Snap to the new position
        transform.position = Vector3.Lerp(oldPos, newPos, 1);
        sameDir = false;
        if(!noFire)
            Instantiate(fire, oldPos - new Vector3(0,0, 5), Quaternion.identity).transform.SetParent(null);
        oldPos = transform.position;
        moving = false;
        noFire = false;
    }

    //Game over if the player touches fire
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("Fire")){
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //If the player made it to the exit with the key
        if(other.gameObject.tag == "Exit" && HasKey(PlayerInventory.GetInventory())){
            RemoveKey(PlayerInventory.GetInventory());
            //Move to a new room if on day 3
            if(GameManager.Instance.day == 3)
                GameManager.Instance.LoadScene("KyOldRoom",true,true);
            //Day 4 - start dialogues based on if Nikko/Simon was with us on previous days
            else if(DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2)
                DialogueManager.Instance.StartConversation("Day4Puzzle3NeutralNikkoEnd");
            else if(DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2)
                DialogueManager.Instance.StartConversation("Day4Puzzle3KyEnd");
            //If neither were with Ky, just move to the rooftop
            else{
                GameManager.Instance.LoadScene("Rooftop", true, true);
                DialogueManager.Instance.puzzleDialogueDone = false;
            }
        }    
    }

    //For the tiles that can be entered more than once
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "SpecialTile"){
            noFire = true;
        }    
    }

}
