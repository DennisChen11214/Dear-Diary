using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Changes scenes to the dining hall or triggers a dialogue when interacted with
public class EnterDiningHall : MonoBehaviour
{
    [SerializeField] Vector3 spawnPos;
    [SerializeField] string sceneToLoad;
    bool canTalkto;
    PlayerControls controls;

    private void Awake() {
        controls = new PlayerControls();
        controls.City.Interact.performed += ctx => StartDialogue();
    }

    void StartDialogue(){
        //Don't start dialogue if the game is paused or we're already in dialogue
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        if(!canTalkto || GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE)
            return;
        switch(GameManager.Instance.day){
            case 1:
                //At night, can't enter the dining hall, dialogue saying it's locked, plays a locked sound
                if(DecisionManager.Instance.puzzleDoneDay1){
                    DialogueManager.Instance.StartConversation("SchoolDoorsNightDialogue");
                    AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.locked);
                }
                //Move to the dining hall scene
                else
                    GameManager.Instance.LoadScene(sceneToLoad, true, true, false, spawnPos.x, spawnPos.y);
                break;
            case 2:
                if(DecisionManager.Instance.puzzleDoneDay2){
                    DialogueManager.Instance.StartConversation("SchoolDoorsNightDialogue");
                    AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.locked);
                }
                else
                    GameManager.Instance.LoadScene(sceneToLoad, true, true, false, spawnPos.x, spawnPos.y);
                break;
            case 3:
                ChangeScene();
                break;
            case 4:
                ChangeScene();
                break;
        }
        canTalkto = false;
    }

    void ChangeScene(){
        //Dialogue triggers if Ky tries to enter without his ID or Nikko on day 3
        if(GameManager.Instance.day == 3 && !(DecisionManager.Instance.withNikkoDay3 || DecisionManager.Instance.hasIdCard)){
            DialogueManager.Instance.StartConversation("DoorBeforeIDDialogue");
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.locked);
        }
        //Can't enter on day 4, dialogue that's triggered depends on if Ky was with Nikko/Simon on previous days
        else if(GameManager.Instance.day == 4 && !DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("DiningHallDay4DiaryRoute");
        }
        else if(GameManager.Instance.day == 4 && DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("DiningHallDay4NeutralNikko");
        }
        else if(GameManager.Instance.day == 4 && !DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("DiningHallDay4NeutralSimon");
        }
        else if(GameManager.Instance.day == 4 && DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("DiningHallDay4Ky");
        }
        //Enter the dining hall
        else{
            GameManager.Instance.LoadScene(sceneToLoad, true, true, false, spawnPos.x, spawnPos.y);
        }
    }
    
    //Player can interact with the astronomy club once they're in range
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            canTalkto = true;
        }
    }

    //Player can no longer interact with the astronomy club if it's out of range
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
