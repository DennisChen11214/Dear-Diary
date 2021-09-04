using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue triggered by interacting with the Lucie
public class LucieDialogue : MonoBehaviour
{
    //Booleans used to identify where this instance of Lucie is
    [SerializeField] bool nearScience;
    [SerializeField] bool nearLibrary;
    [SerializeField] bool inDining;
    [SerializeField] Vector3 sciencePos;
    [SerializeField] Vector3 gymPos;
    bool canTalkto;
    PlayerControls controls;

    private void Awake() {
        controls = new PlayerControls();
        controls.City.Interact.performed += ctx => StartDialogue();
        DialogueManager.onLucieFainted += Faint;
    }

    private void Start() {
        switch(GameManager.Instance.day){
            //Only active near the science building on day 1 in the afternoon
            case 1:
                transform.position = sciencePos;
                if(!nearScience || DecisionManager.Instance.puzzleDoneDay1){
                    gameObject.SetActive(false);
                }
                break;
            //Only active near the libray on day 2 in the afternoon
            case 2:
                if(!nearLibrary || DecisionManager.Instance.puzzleDoneDay2){
                    gameObject.SetActive(false);
                }
                break;
            //Only active near in the dining hall on day 3 in the afternoon
            case 3:
                if(!inDining || DecisionManager.Instance.puzzleDoneDay3){
                    gameObject.SetActive(false);
                }
                break;
            //Only active near the gym building on day 4
            case 4:
                if(!nearScience){
                    gameObject.SetActive(false);
                }
                transform.position = gymPos;
                break;
        }
    }
    void StartDialogue(){
        //Don't start dialogue if the game is paused or we're already in dialogue
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        if(!canTalkto || GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE)
            return;
        //Different dialogues for each day, time of day, if we're with Nikko, and depending on if we talked to her already
        switch(GameManager.Instance.day){
            case 1:
                if(!DecisionManager.Instance.talkedToLucieDay1){
                    DialogueManager.Instance.StartConversation("LucieDialogueDay1");
                    DecisionManager.Instance.talkedToLucieDay1 = true;
                }
                else if(!DecisionManager.Instance.puzzleDoneDay1){
                    DialogueManager.Instance.StartConversation("LucieDialogueDay1Multiple");
                }
                break;
            case 2:
                DialogueManager.Instance.StartConversation("LucieDialogueDay2");
                break;
            case 3:
                if(!DecisionManager.Instance.talkedToLucieDay3 && DecisionManager.Instance.withNikkoDay3){
                    DialogueManager.Instance.StartConversation("DiningHallLucieDialogue");
                    DecisionManager.Instance.talkedToLucieDay3 = true;
                }
                else if(DecisionManager.Instance.withNikkoDay3){
                    DialogueManager.Instance.StartConversation("DiningHallLucieDialogue2");
                }
                break;
            case 4:
                if(!DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2)
                    DialogueManager.Instance.StartConversation("LucieDay4DiaryRoute");
                else
                    DialogueManager.Instance.StartConversation("LucieDay4NeutralKyDialogue");
                break;
        }
        canTalkto = false;
    }

    //Plays a faint animation when talking to her on day 3 with Nikko
    private void Faint(){
        GetComponent<Animator>().Play("Faint");
    }

    //Player can interact with Lucie once they're in range
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            canTalkto = true;
        }
    }

    //Player can no longer interact with Lucie if it's out of range
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
        DialogueManager.onLucieFainted -= Faint;
    }
}
