using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Dialogue trigger to enter the fire puzzle, located at the kitchen in the dining hall
public class FirePuzzleTrigger : MonoBehaviour
{
    bool canTalkto;
    PlayerControls controls;

    private void Awake() {
        controls = new PlayerControls();
        controls.City.Interact.performed += ctx => StartDialogue();
        DialogueManager.onPuzzleEntered += LoadPuzzle;
    }
    private void Start() {
        //Can't enter again once the puzzle is done
        if(DecisionManager.Instance.puzzleDoneDay3)
            Destroy(gameObject);
    }

    //Load in a puzzle depending if Ky is with Nikko or not
    private void LoadPuzzle() {
        if(DecisionManager.Instance.withNikkoDay3){
            GameManager.Instance.LoadScene("KyPathFire", true, true);
        }
        else{
            GameManager.Instance.LoadScene("DiaryPathFire", true, true);
        }
    }

    //Different dialogues depending if the player is with Nikko and if it's a repeated interaction
    void ChangeScene(){
        if(GameManager.Instance.CurrentGameState != GameManager.GameState.DIALOGUE && 
                        !DecisionManager.Instance.interactedWithKitchen && DecisionManager.Instance.withNikkoDay3){
            DialogueManager.Instance.StartConversation("DiningHallKitchenDialogue");
            DecisionManager.Instance.interactedWithKitchen = true;
        }
        else if(GameManager.Instance.CurrentGameState != GameManager.GameState.DIALOGUE && 
                        DecisionManager.Instance.interactedWithKitchen && DecisionManager.Instance.withNikkoDay3){
            DialogueManager.Instance.StartConversation("DiningHallKitchenDialogue2");
        }
        else if(GameManager.Instance.CurrentGameState != GameManager.GameState.DIALOGUE && 
                        !DecisionManager.Instance.interactedWithKitchen && !DecisionManager.Instance.withNikkoDay3){
            DialogueManager.Instance.StartConversation("DiningHallKitchenAlone");
            DecisionManager.Instance.interactedWithKitchen = true;
        }
        else if(GameManager.Instance.CurrentGameState != GameManager.GameState.DIALOGUE && 
                        DecisionManager.Instance.interactedWithKitchen && !DecisionManager.Instance.withNikkoDay3){
            DialogueManager.Instance.StartConversation("DiningHallKitchenAlone2");
        }
    }
    void StartDialogue(){
        //Don't start dialogue if the game is paused or we're already in dialogue
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        if(!canTalkto || GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE)
            return;
        if(GameManager.Instance.day == 3){
            ChangeScene();
        }
        canTalkto = false;
    }

    //Player can interact with the kitchen once they're in range
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            canTalkto = true;
        }
    }

    //Player can't interact with the kitchen once they're out of range
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
