using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue triggered by interacting with the chef in the dining hall
public class ChefDialogue : MonoBehaviour
{
    bool canTalkto;
    PlayerControls controls;

    private void Awake() {
        controls = new PlayerControls();
        controls.City.Interact.performed += ctx => StartDialogue();
    }

    private void Start() {
        switch(GameManager.Instance.day){
            case 1:
                gameObject.SetActive(false);
                break;
            //Only active on the second day in the afternoon
            case 2:
                if(DecisionManager.Instance.puzzleDoneDay2)
                    gameObject.SetActive(false);
                break;
            case 3:
                gameObject.SetActive(false);
                break;
            case 4:
                gameObject.SetActive(false);
                break;
        }
    }
    void StartDialogue(){
        //Don't start dialogue if the game is paused or we're already in dialogue
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        if(!canTalkto || GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE)
            return;
        switch(GameManager.Instance.day){
            case 1:
                break;
            case 2:
                DialogueManager.Instance.StartConversation("ChefDialogueDay2");
                break;
            case 3:
                break;
            case 4:
                break;
        }
        canTalkto = false;
    }
    //Player can interact with the chef once they're in range
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            canTalkto = true;
        }
    }

    //Player can no longer interact with the chef if it's out of range
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            canTalkto = false;
        }
    }
    private void OnEnable() {
        controls.City.Enable();
    }

    private void OnDisable() {
        controls.City.Disable();
    }
}
