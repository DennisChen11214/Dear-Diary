using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue triggered by interacting with the gym
public class GymDialogue : MonoBehaviour
{
    bool canTalkto;
    PlayerControls controls;

    private void Awake() {
        controls = new PlayerControls();
        controls.City.Interact.performed += ctx => StartDialogue();
        DialogueManager.onPuzzleEntered += LoadPuzzle;
    }

    //Switch scenes to the ice puzzle
    private void LoadPuzzle() {
        if(DecisionManager.Instance.withSimonDay2){
            GameManager.Instance.LoadScene("KyPathIce", true, true);
        }
        else{
            GameManager.Instance.LoadScene("DiaryPathIce", true, true);
        }
    }

    //Start the dialogues that cause the ice puzzle to be loaded in
    void ChangeScene(){
        if(!DecisionManager.Instance.interactedWithGym && DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("GymDoorsSimonDialogue");
            DecisionManager.Instance.interactedWithGym = true;
        }
        else if(DecisionManager.Instance.interactedWithGym && DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("GymDoorsSimonDialogue");
        }
        else if(!DecisionManager.Instance.interactedWithGym && !DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("GymDoorsAfterPassAlone");
            DecisionManager.Instance.interactedWithGym = true;
        }
        else if(DecisionManager.Instance.interactedWithGym && !DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("GymDoorsAfterPassAloneMultiple");
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
                    DialogueManager.Instance.StartConversation("GymDoorsNightDialogue");
                    AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.locked);
                }
                break;
            case 2:
                if(DecisionManager.Instance.puzzleDoneDay2){
                    DialogueManager.Instance.StartConversation("GymDoorsNightDialogue");
                    AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.locked);
                }
                else if(DecisionManager.Instance.withSimonDay2 && !DecisionManager.Instance.wentToClassDay2){
                    DialogueManager.Instance.StartConversation("GymDoorsSimonEarlyDay2");
                }
                else if(!DecisionManager.Instance.withSimonDay2 && !DecisionManager.Instance.talkedToCoachDay2){
                    DialogueManager.Instance.StartConversation("GymDoorsBeforePassAlone");
                }
                else{
                    ChangeScene();
                }
                break;
            case 3:
                if(DecisionManager.Instance.puzzleDoneDay3){
                    DialogueManager.Instance.StartConversation("GymDoorsNightDialogue");
                    AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.locked);
                }
                break;
            case 4:
                if(!DecisionManager.Instance.withSimonDay2 && !DecisionManager.Instance.withNikkoDay3)
                    DialogueManager.Instance.StartConversation("ClassDay4DiaryRoute");
                else
                    DialogueManager.Instance.StartConversation("ClassDay4NeutralKy");
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
