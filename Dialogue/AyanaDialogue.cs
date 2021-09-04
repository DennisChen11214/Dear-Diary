using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue triggered by interacting with Ayana
public class AyanaDialogue : MonoBehaviour
{
    //Booleans used to identify where this instance of Ayana is
    [SerializeField] bool nearDining;
    [SerializeField] bool nearArt;
    [SerializeField] bool nearLibrary;
    bool canTalkto;
    PlayerControls controls;

    private void Awake() {
        controls = new PlayerControls();
        controls.City.Interact.performed += ctx => StartDialogue();
    }

    private void Start() {
        switch(GameManager.Instance.day){
            case 1:
                //Only active near the art building on day 1
                if(!nearArt){
                    gameObject.SetActive(false);
                }
                break;
            case 2:
                //Only active near the dining hall in the afternoon on day 2
                if(!nearDining || DecisionManager.Instance.puzzleDoneDay2){
                    gameObject.SetActive(false);
                }
                break;
            case 3:
                //Active near the art building in the afternoon and near the library at night on day 3
                if((!nearArt && !nearLibrary) || (nearArt && DecisionManager.Instance.puzzleDoneDay3) ||
                        (nearLibrary && !DecisionManager.Instance.puzzleDoneDay3))
                    gameObject.SetActive(false);
                break;
            case 4:
                //Not active on campus on day 4
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
        //Different dialogues for each day, time of day, and depending on if we talked to her already
        switch(GameManager.Instance.day){
            case 1:
                if(!DecisionManager.Instance.talkedToAyanaDay1 && !DecisionManager.Instance.puzzleDoneDay1){
                    DialogueManager.Instance.StartConversation("AyanaDialogueDay1Start");
                    DecisionManager.Instance.talkedToAyanaDay1 = true;
                }
                else if(DecisionManager.Instance.puzzleDoneDay1){
                    DialogueManager.Instance.StartConversation("Day1AyanaAfterPuzzle");
                }
                else {
                    DialogueManager.Instance.StartConversation("AyanaDay1StartMultiple");
                }
                break;
            case 2:
                if(!DecisionManager.Instance.talkedToAyanaDay2 && DecisionManager.Instance.withSimonDay2){
                    DialogueManager.Instance.StartConversation("TalkAyanaSimonEarlyDay2");
                    DecisionManager.Instance.talkedToAyanaDay2 = true;
                }
                else if(!DecisionManager.Instance.talkedToAyanaDay2 && !DecisionManager.Instance.withSimonDay2){
                    DialogueManager.Instance.StartConversation("AyanaDay2Alone");
                    DecisionManager.Instance.talkedToAyanaDay2 = true;
                }
                else if(DecisionManager.Instance.withSimonDay2){
                    DialogueManager.Instance.StartConversation("TalkAyanaSimonEarlyDay2Multiple");
                }
                else{
                    DialogueManager.Instance.StartConversation("AyanaDay2AloneMultiple");
                }
                break;
            case 3:
                if(!DecisionManager.Instance.puzzleDoneDay3)
                    DialogueManager.Instance.StartConversation("AyanaDialogueDay3");
                else
                    DialogueManager.Instance.StartConversation("AyanaDay3NightDialogue");
                break;
            case 4:
                break;
        }
        canTalkto = false;
    }


    //Player can interact with Ayana once they're in range
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            canTalkto = true;
        }
    }

    //Player can no longer interact with Ayana if it's out of range
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
