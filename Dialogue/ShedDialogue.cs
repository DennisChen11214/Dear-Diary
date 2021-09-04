using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue triggered by interacting with the shed
public class ShedDialogue : MonoBehaviour
{
    bool canTalkto;
    PlayerControls controls;

    private void Awake() {
        controls = new PlayerControls();
        controls.City.Interact.performed += ctx => StartDialogue();
        DialogueManager.onPuzzleEntered += LoadPuzzle;
    }
    private void Start() {
        //Only active on day 1 in the afternoon
        if(DecisionManager.Instance.puzzleDoneDay1)
            Destroy(gameObject);
    }

    private void LoadPuzzle() {
        GameManager.Instance.LoadScene("Day1Puzzle", true, true);
    }

    //Start a dialogue that could lead into the puzzle being loaded in
    void ChangeScene(){
        if(GameManager.Instance.CurrentGameState != GameManager.GameState.DIALOGUE && 
                        !DecisionManager.Instance.interactedWithShed){
            DialogueManager.Instance.StartConversation("Day1ShedDialogue");
            DecisionManager.Instance.interactedWithShed = true;
        }
        else if(GameManager.Instance.CurrentGameState != GameManager.GameState.DIALOGUE && 
                        DecisionManager.Instance.interactedWithShed){
            DialogueManager.Instance.StartConversation("Day1ShedDialogueMultiple");
        }
    }
    void StartDialogue(){
        //Don't start dialogue if the game is paused or we're already in dialogue
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        if(!canTalkto || GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE)
            return;
        if(GameManager.Instance.day == 1){
            ChangeScene();
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

    private void OnDestroy() {
        DialogueManager.onPuzzleEntered -= LoadPuzzle;
    }
}
