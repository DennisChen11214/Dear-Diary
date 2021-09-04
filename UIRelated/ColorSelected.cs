using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorSelected : MonoBehaviour
{
    //The button's text color initially
    Color initialColor;

    private void Start() {
        initialColor = GetComponentInChildren<Text>().color;
    }

    void Update()
    {
        //Sets the button's text color to the button's selected color
        if(EventSystem.current && EventSystem.current.currentSelectedGameObject == gameObject){
            GetComponentInChildren<Text>().color = GetComponent<Button>().colors.selectedColor;
        }
        //Sets the button's text color to the initial color when not selected
        else if(EventSystem.current){
            GetComponentInChildren<Text>().color = initialColor;
        }
    }
}
