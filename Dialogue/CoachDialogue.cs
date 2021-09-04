using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue triggered by interacting with the coach
public class CoachDialogue : MonoBehaviour
{
    bool canTalkto;
    PlayerControls controls;
    //Original position is near the dining hall, this is his position near the gym
    [SerializeField] Vector3 nearGym;
    private void Awake() {
        controls = new PlayerControls();
        controls.City.Interact.performed += ctx => StartDialogue();
    }

    private void Start() {
        switch(GameManager.Instance.day){
            case 1:
                gameObject.SetActive(false);
                break;
            //Active on day 2 in the afternoon near the dining hall
            case 2:
                if(DecisionManager.Instance.puzzleDoneDay2)
                    gameObject.SetActive(false);
                break;
            //Active on day 3 in the afternoon near the gym
            case 3:
                transform.position = nearGym;
                if(DecisionManager.Instance.puzzleDoneDay3)
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
        //Different dialogues for each day depending on whether we were with Simon or talked to him already
        switch(GameManager.Instance.day){
            case 1:
                break;
            case 2:
                if(DecisionManager.Instance.withSimonDay2 && !DecisionManager.Instance.wentToClassDay2 && 
                   !DecisionManager.Instance.talkedToCoachDay2){
                    DialogueManager.Instance.StartConversation("CoachDialogueWithSimonDay2");
                    DecisionManager.Instance.talkedToCoachDay2 = true;
                }
                else if(!DecisionManager.Instance.withSimonDay2 && !DecisionManager.Instance.talkedToCoachDay2){
                    DialogueManager.Instance.StartConversation("CoachDialogueAloneDay2");
                    DecisionManager.Instance.talkedToCoachDay2 = true;
                }
                else if(!DecisionManager.Instance.withSimonDay2 && DecisionManager.Instance.talkedToCoachDay2){
                    DialogueManager.Instance.StartConversation("CoachDialogueAloneDay2Multiple");
                }
                break;
            case 3:
                DialogueManager.Instance.StartConversation("KyCoachDialogueDay3");
                break;
            case 4:
                break;
        }
        canTalkto = false;
    }
    //Player can interact with the coach once they're in range
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            canTalkto = true;
        }
    }

    //Player can no longer interact with the coach if it's out of range
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
