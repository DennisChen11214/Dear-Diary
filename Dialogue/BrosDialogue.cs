using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue triggered by interacting with the bro NPCs
public class BrosDialogue : MonoBehaviour
{
    bool canTalkto;
    PlayerControls controls;

    private void Awake() {
        controls = new PlayerControls();
        controls.City.Interact.performed += ctx => StartDialogue();
    }

    private void Start() {
        switch(GameManager.Instance.day){
            //Only active on the first day in the afternoon
            case 1:
                if(DecisionManager.Instance.puzzleDoneDay1)
                    gameObject.SetActive(false);
                break;
            case 2:
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
                DialogueManager.Instance.StartConversation("GymEntranceDialogueDay1");
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
        canTalkto = false;
    }
    //Player can interact with the building once they're in range
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            canTalkto = true;
        }
    }

    //Player can no longer interact with the building if it's out of range
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
