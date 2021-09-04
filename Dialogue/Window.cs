using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue triggered by interacting with the window in Ky's room
public class Window : MonoBehaviour
{
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
        //Ky's reaction to the weather outside every day
        switch(GameManager.Instance.day){
            case 1:
                DialogueManager.Instance.StartConversation("KyDormWindow");
                break;
            case 2:
                DialogueManager.Instance.StartConversation("KyDormWindowDay2");
                break;
            case 3:
                DialogueManager.Instance.StartConversation("KyDormWindowDay3");
                break;
            case 4:
                DialogueManager.Instance.StartConversation("KyDormWindowDay4");
                break;
        }
        canTalkto = false;
    }

    //Player can interact with the window once they're in range
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            canTalkto = true;
        }
    }

    //Player can no longer interact with the window if it's out of range
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
