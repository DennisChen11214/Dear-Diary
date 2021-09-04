using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Automatically sets the first choice to be selected/highlighted
public class Choices : MonoBehaviour
{
    [SerializeField] GameObject firstButtonSelected;
    GameObject currentSelected;

    private void Update() {
        if(EventSystem.current && EventSystem.current.currentSelectedGameObject == null){
            EventSystem.current.SetSelectedGameObject(currentSelected);
        }
        else if(EventSystem.current){
            currentSelected = EventSystem.current.currentSelectedGameObject;
        }
    }

    private void OnEnable() {
        EventSystem.current.SetSelectedGameObject(firstButtonSelected);
    }
}
