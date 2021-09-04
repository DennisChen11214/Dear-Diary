using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] GameObject firstButtonSelected;
    GameObject currentSelected;

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
