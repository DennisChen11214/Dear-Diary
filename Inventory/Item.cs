using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Item script put onto items that can be picked up
public class Item: MonoBehaviour {

    public ItemInformation itemInformation;

    private void Start() {
        //Set the day that this item shows up
        itemInformation.day = GameManager.Instance.day;
        //The puzzle scene on day 4 determines what diary page you get
        if(SceneManager.GetActiveScene().name == "Day4FirstPuzzle")
            itemInformation.endDayPuzzle = 1;
        if(SceneManager.GetActiveScene().name == "Day4SecondPuzzle")
            itemInformation.endDayPuzzle = 2;
        if(SceneManager.GetActiveScene().name == "Day4ThirdPuzzle")
            itemInformation.endDayPuzzle = 3;
        //Destroy the ID card if you already have it or are with Nikko or if it's not day 3
        if(itemInformation.itemType == ItemInformation.ItemType.IDCard && DecisionManager.Instance.withNikkoDay3){
            Destroy(gameObject);
        }
        if(itemInformation.itemType == ItemInformation.ItemType.IDCard && (DecisionManager.Instance.hasIdCard || 
                                                                            GameManager.Instance.day != 3)){
            Destroy(gameObject);
        }
        //Destroy the diary in Ky's room after picking it up each day
        if(itemInformation.itemType == ItemInformation.ItemType.Diary && 
            ((DecisionManager.Instance.diaryCheckedDay1 && GameManager.Instance.day == 1) ||
            (DecisionManager.Instance.diaryCheckedDay2 && GameManager.Instance.day == 2)|| 
            (DecisionManager.Instance.diaryCheckedDay3 && GameManager.Instance.day == 3)||
            (DecisionManager.Instance.diaryCheckedDay4 && GameManager.Instance.day == 4))){
            Destroy(gameObject);
        }
    }

}