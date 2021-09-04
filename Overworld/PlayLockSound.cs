using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Plays the sound of a lock if the player interacts with this object
public class PlayLockSound : MonoBehaviour
{
    bool canInteractWith;
    PlayerControls controls;

    private void Awake() {
        controls = new PlayerControls();
        controls.City.Interact.performed += ctx => StartDialogue();
    }

    void StartDialogue(){
        //Don't start dialogue if the game is paused or we're already in dialogue
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        if(!canInteractWith || GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE)
            return;
        //Play the locked sound
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.locked);
        canInteractWith = false;
    }

    //Player can interact with the door once they're in range
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            canInteractWith = true;
        }
    }

    //Player can't interact with the door if they're not in range
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            canInteractWith = false;
        }
    }
    private void OnEnable() {
        controls.City.Enable();
    }

    private void OnDisable() {
        controls.City.Disable();
    }
}
