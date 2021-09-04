using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject firstButtonSelected;
    GameObject currentSelected;

    //Initialize everything that needs to be done for a fresh start to the game
    public void StartGame(){
        GameManager.Instance.day = 1;
        DecisionManager.Instance.Clear();
        PlayerInventory.ClearInventory();
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.buttonPress);
        DialogueManager.Instance.puzzleDialogueDone = false;
        Diary.ClearDiary();
        GameManager.Instance.LoadScene("KyRoom", false, true);
        KyMovementCity.Face(-2);
    }

    //Load the latest save file
    public void Load(){
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.buttonPress);
        GameManager.Instance.LoadGame();
    }

    private void Update() {
        //Sets the button's text color to the button's selected color
        if(EventSystem.current && EventSystem.current.currentSelectedGameObject == null){
            EventSystem.current.SetSelectedGameObject(currentSelected);
        }
        //Sets the button's text color to the initial color when not selected
        else if(EventSystem.current){
            currentSelected = EventSystem.current.currentSelectedGameObject;
        }
    }

    //Set the selected button to the one selected in the editor
    private void OnEnable() {
        EventSystem.current.SetSelectedGameObject(firstButtonSelected);
    }

    //Quit the game
    public void Quit(){
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.buttonPress);
        Application.Quit();
    }
}
