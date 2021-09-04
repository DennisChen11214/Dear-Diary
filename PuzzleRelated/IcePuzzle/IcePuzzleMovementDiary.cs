using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IcePuzzleMovementDiary : PuzzleMovement
{
    #region Public Variables
    #endregion
    #region Serialized Fields
    #endregion

    #region Private Variables
    int numSteps;

    bool faceAndMove = false;
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
        if(directionDetect != Vector2.zero && !moving){
            CalcDirection();
        }
        //First time hitting a direction makes the character face that way but not move
        else if(!moving)
            faceAndMove = true;
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
        RaycastHit2D hit2D = new RaycastHit2D();
        if(!moving){
            animSameDir = false;
            animator.SetBool("idle", true);
            animator.SetFloat("yMovement", 0);
            animator.SetBool("yMoving", false);
            animator.SetBool("xMovement", false);
            numSteps = 1;
            //Check where the character would end up if they slid across the ice in one direction
            while(!hit2D && directionMoving != Vector2.zero){
                hit2D = Physics2D.Raycast(transform.position, directionMoving, stepDistance * numSteps, 1 << 9);
                numSteps++;
                if(hit2D){
                    string tag = hit2D.collider.gameObject.tag;
                    //If we hit a special tile, hole, or exit, move one more tile so we land on it
                    if(tag == "SpecialTile" || tag == "Hole" || tag == "Exit"){
                        numSteps++;
                    }
                }
            }
            numSteps -= 2;
        }
        //If the character moves in the same direction they're facing
        if(sameDir && !moving && faceAndMove){
            moving = true;
            newPos = transform.position + (Vector3)directionMoving * stepDistance * numSteps;
            StartCoroutine(Move(null, oldPos, newPos));
        }
        else if(sameDir && hit2D){
            sameDir = false;
        }
        if(directionDetect == Vector2.zero){ 
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
        //If we move in the same direction we're facing
        if(oldDir == DirectionFacing && faceAndMove){
            sameDir = true;
        }
        //If we try moving in a different direction we're facing
        else if(oldDir != DirectionFacing){
            faceAndMove = false;
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
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.iceMove);
        //Move the character to the next wall
        while (timeElapsed < 1)
        {
            transform.position = Vector3.Lerp(oldPos, newPos, timeElapsed);
            timeElapsed += Time.deltaTime / numSteps * 10;
            yield return null;
        }
        transform.position = Vector3.Lerp(oldPos, newPos, 1);
        AudioManager.Instance.Stop();
        sameDir = false;
        oldPos = transform.position;
        moving = false;
    }

    //Game over if the character falls into a hole
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Hole"){
            GameManager.Instance.GameOver();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        //If the player made it to the exit with the key
        if(other.gameObject.tag == "Exit" && HasKey(PlayerInventory.GetInventory())){
            RemoveKey(PlayerInventory.GetInventory());
            //Start a conversation at the end of the puzzle and add a page to the diary
            if(GameManager.Instance.day == 2){
                DialogueManager.Instance.StartConversation("Day2PuzzleEndAlone");
                ItemInformation diaryEndPage = new ItemInformation();
                diaryEndPage.itemType = ItemInformation.ItemType.Diary;
                diaryEndPage.day = 2;
                diaryEndPage.end = true;
                Diary.AddPage(diaryEndPage);
            }
            //Day 4 - start dialogues based on if Nikko/Simon was with us on previous days
            else if(GameManager.Instance.day == 4){
                if(!DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2)
                    DialogueManager.Instance.StartConversation("Day4Puzzle2NeutralSimonEnd");
                else if(DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2)
                    DialogueManager.Instance.StartConversation("Day4Puzzle2KyEnd");
                else{
                    DialogueManager.Instance.puzzleDialogueDone = false;
                    GameManager.Instance.LoadScene("Day4ThirdPuzzle", true, true);
                }
            }
        }    
    }

}
