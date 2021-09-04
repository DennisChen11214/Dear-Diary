using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue triggered by interacting with Nikko on day 3
public class NikkoLecture : MonoBehaviour
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
            case 2:
                gameObject.SetActive(false);
                break;
            //Only active if Ky doesn't have his ID and Nikko isn't following Ky
            case 3:
                if(DecisionManager.Instance.hasIdCard || DecisionManager.Instance.withNikkoDay3){
                    gameObject.SetActive(false);
                }
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
                break;
            //Different dialogues for the first time and repeating times
            case 3:
                if(!DecisionManager.Instance.talkedToNikkoDay3){
                    DialogueManager.Instance.StartConversation("NikkoBeforeLectureDialogue");
                    DecisionManager.Instance.talkedToNikkoDay3 = true;
                }
                else {
                    DialogueManager.Instance.StartConversation("NikkoAfterChooseNo");
                }
                break;
            case 4:
                break;
        }
        canTalkto = false;
    }

    //Player can interact with Nikko once they're in range
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            canTalkto = true;
        }
    }

    //Player can interact with Nikko once they're in range
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
