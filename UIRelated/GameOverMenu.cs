using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] GameObject firstButtonSelected;
    GameObject currentSelected;

    //Go back to the main menu
    public void QuitGame(){
        gameObject.SetActive(false);
        GameManager.Instance.LoadScene("Main Menu");
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
}
