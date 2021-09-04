using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Script for Nikko/Simon/Nova to follow behind Ky on campus
public class FollowKy : MonoBehaviour
{
    private GameObject ky;
    private Rigidbody2D rb;
    private Animator animator;
    private Positions PlayerPositions;
    private float timeTracker;
    private float prevTime;
    public static bool notFollow;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayerPositions = new Positions();
    }

    private void FixedUpdate() {
        if(GameManager.Instance.day != 2)
            notFollow = false;
        if(notFollow){
            GetComponent<BoxCollider2D>().enabled = true;
        }
        //Don't follow if Ky decided not to hang out with Nikko/Simon
        if(GameManager.Instance.day == 2 && !DecisionManager.Instance.withSimonDay2){
            return;
        }
        if(GameManager.Instance.day == 3 && !DecisionManager.Instance.withNikkoDay3){
            return;
        }
        //If Ky is moving
        if(!ky.GetComponent<Animator>().GetBool("idle")){
            //Add a position frame at Ky's current position
            Vector2 playerpos = ky.transform.position;
            timeTracker += Time.time - prevTime;
            prevTime = Time.time;
            PlayerPositions.x.AddKey(timeTracker, playerpos.x);
            PlayerPositions.y.AddKey(timeTracker, playerpos.y);
            //Get the position of where Ky was 0.2 seconds ago.
            float x = PlayerPositions.x.Evaluate(timeTracker - 0.20f);
            float y = PlayerPositions.y.Evaluate(timeTracker - 0.20f);
            Vector3 newpos = new Vector2(x, y);
            //Set the animation according to the direction between the current position and new position
            animator.SetBool("idle", false);
            if(Mathf.Abs(newpos.x - transform.position.x) > 0.02f){
                animator.SetBool("xMovement",true);
                if(newpos.x - transform.position.x > 0.02f){
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else if(newpos.x - transform.position.x < -0.02f){
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else{
                animator.SetBool("xMovement",false);
            }
            if(Mathf.Abs(newpos.y - transform.position.y) > 0.02f){
                animator.SetBool("yMoving", true);
            }
            else{
                animator.SetBool("yMoving",false);
            }
            animator.SetFloat("yMovement", newpos.y - transform.position.y);
            if(transform.position.y - ky.transform.position.y > 0.02f){
                newpos.z = 1;
            }
            else if(transform.position.y - ky.transform.position.y < - 0.02f){
                newpos.z = -1;
            }
            //Move to the new position
            transform.position = newpos;
        }
        else{
            animator.SetBool("idle", true);
            animator.SetBool("xMovement",false);
            animator.SetBool("yMoving",false);
            animator.SetFloat("yMovement",0);
            prevTime = Time.time;
        }
    }

    //Set the target to Ky and add the initial frames
    public void SetTarget(GameObject target){
        ky = target;
        PlayerPositions = new Positions();
        PlayerPositions.x.AddKey(timeTracker, transform.position.x);
        PlayerPositions.y.AddKey(timeTracker, transform.position.y);
        timeTracker += 0.15f;
        PlayerPositions.x.AddKey(timeTracker, ky.transform.position.x);
        PlayerPositions.y.AddKey(timeTracker, ky.transform.position.y);
    }
}
