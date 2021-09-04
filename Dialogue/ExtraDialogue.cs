using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue triggered by interacting with the object, used for objects in Ky's room
public class ExtraDialogue : MonoBehaviour
{
    [SerializeField] string dialogueName;
    PlayerControls controls;
    bool inRange;
    private void Awake() {
        controls = new PlayerControls();
        controls.City.Interact.performed += ctx => PlayDialogue();
    }

    void PlayDialogue(){
        //Don't start dialogue if the game is paused or we're already in dialogue
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        //Play the dialogue
        if(inRange){
            if(GameManager.Instance.CurrentGameState != GameManager.GameState.DIALOGUE){
                DialogueManager.Instance.StartConversation(dialogueName);
            }
            inRange = false;
        }
    }

    //Player can interact with the object once they're in range
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
            inRange = true;
    }

    //Player can no longer interact with the object if it's out of range
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player")
            inRange = false;
    }
    private void OnEnable() {
        controls.City.Enable();
    }

    private void OnDisable() {
        controls.City.Disable();
    }
}
