using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue triggered by interacting with the library
public class LibraryDialogue : MonoBehaviour
{
    bool canTalkto;
    PlayerControls controls;

    private void Awake() {
        controls = new PlayerControls();
        controls.City.Interact.performed += ctx => StartDialogue();
        DialogueManager.onPuzzleEntered += LoadPuzzle;
    }

    private void LoadPuzzle() {
        GameManager.Instance.LoadScene("Day4FirstPuzzle", true, true);
    }

    //On the 4th day, enter the puzzle after these converstations, which depend of previous decisions with Nikko/Simon
    void ChangeScene(){
        if(!DecisionManager.Instance.withSimonDay2 && !DecisionManager.Instance.withNikkoDay3){
            DialogueManager.Instance.StartConversation("LibraryDay4DiaryRoute");
        }
        else if(DecisionManager.Instance.withSimonDay2 && !DecisionManager.Instance.withNikkoDay3){
            DialogueManager.Instance.StartConversation("LibraryDay4NeutralSimon");
        }
        else if(!DecisionManager.Instance.withSimonDay2 && DecisionManager.Instance.withNikkoDay3){
            DialogueManager.Instance.StartConversation("LibraryDay4NeutralNikko");
        }
        else if(DecisionManager.Instance.withSimonDay2 && DecisionManager.Instance.withNikkoDay3){
            DialogueManager.Instance.StartConversation("LibraryDay4Ky");
        }
    }
    void StartDialogue(){
        //Don't start dialogue if the game is paused or we're already in dialogue
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        if(!canTalkto || GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE)
            return;
        //Different dialogues for each day, time of day, and depending on whether we were with Simon/Nikko on their respective days
        switch(GameManager.Instance.day){
            case 1:
                if(DecisionManager.Instance.puzzleDoneDay1){
                    DialogueManager.Instance.StartConversation("SchoolDoorsNightDialogue");
                    //Plays a sound if it's locked
                    AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.locked);
                }
                else{
                    DialogueManager.Instance.StartConversation("LibraryEntranceDialogueDay1");
                }
                break;
            case 2:
                if(DecisionManager.Instance.puzzleDoneDay2){
                    DialogueManager.Instance.StartConversation("SchoolDoorsNightDialogue");
                    AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.locked);
                }
                else if(DecisionManager.Instance.withSimonDay2 && DecisionManager.Instance.wentToClassDay2){
                    DialogueManager.Instance.StartConversation("SimonLockedAfterLectureDialogue");
                }
                else if(DecisionManager.Instance.withSimonDay2 && !DecisionManager.Instance.wentToClassDay2){
                    DialogueManager.Instance.StartConversation("SimonLockedBeforeLectureDialogue");
                }
                else if(!DecisionManager.Instance.withSimonDay2){
                    DialogueManager.Instance.StartConversation("LibraryDialogueAloneDay2");
                }
                break;
            case 3:
                if(DecisionManager.Instance.puzzleDoneDay3){
                    DialogueManager.Instance.StartConversation("SchoolDoorsNightDialogue");
                    AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.locked);
                }
                else if((!DecisionManager.Instance.hasIdCard && !DecisionManager.Instance.withNikkoDay3)){
                    DialogueManager.Instance.StartConversation("DoorBeforeIDDialogue");
                    AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.locked);
                }
                else if(DecisionManager.Instance.withNikkoDay3)
                    DialogueManager.Instance.StartConversation("CampusBuildingDialogue");
                else if(DecisionManager.Instance.hasIdCard)
                    DialogueManager.Instance.StartConversation("CampusBuildingDialogueID");
                break;
            case 4:
                ChangeScene();
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

    private void OnDestroy() {
        DialogueManager.onPuzzleEntered -= LoadPuzzle;
    }
}
