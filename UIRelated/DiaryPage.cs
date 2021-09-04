using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The actual diary page that shows up on screen
public class DiaryPage : MonoBehaviour
{
    PlayerControls controls;
    private void Awake()
    {
        controls = new PlayerControls();
        controls.UI.ClosePopUpPage.performed += ctx => ClosePage();
    }

    private void ClosePage(){
        //Don't close if game is paused
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        //Changes game state back to what it was before
        UIManager.Instance.ToggleDiaryPage();
        //Handle the dialogues for all the cutscene page popups
        if(GameManager.Instance.day == 1 && UIManager.Instance.popUp){
            if(!DecisionManager.Instance.beforeClassDay1)
                DialogueManager.Instance.StartConversation("diaryDay1Page");
            else   
                DialogueManager.Instance.StartConversation("Day1EndEntry");
        }
        else if(GameManager.Instance.day == 2 && UIManager.Instance.popUp){
            if(!DecisionManager.Instance.beforeClassDay2)
                DialogueManager.Instance.StartConversation("diaryDay2Page");
            else if(DecisionManager.Instance.withSimonDay2)
                DialogueManager.Instance.StartConversation("Day2EndEntrySimon");
            else
                DialogueManager.Instance.StartConversation("Day2EndEntryAlone");
        }
        else if(GameManager.Instance.day == 3 && UIManager.Instance.popUp){
            if(!DecisionManager.Instance.beforeClassDay3)
                DialogueManager.Instance.StartConversation("KyAfterSeeFirePage");
            else if(DecisionManager.Instance.withSimonDay2 && DecisionManager.Instance.withNikkoDay3)
                DialogueManager.Instance.StartConversation("Day3EndEntryNikkoSimon");
            else if(DecisionManager.Instance.withSimonDay2)
                DialogueManager.Instance.StartConversation("Day3EndEntrySimon");
            else if(DecisionManager.Instance.withNikkoDay3)
                DialogueManager.Instance.StartConversation("Day3EndEntryNikko");
            else
                DialogueManager.Instance.StartConversation("Day3EndEntryAlone");
        }
        else if(GameManager.Instance.day == 4 && UIManager.Instance.popUp && 
                !DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
            if(!DecisionManager.Instance.beforeClassDay4)
                DialogueManager.Instance.StartConversation("diaryDay4PageDiaryRoute");
            else
                DialogueManager.Instance.StartConversation("Day4EndAlone");
        }
        else if(GameManager.Instance.day == 4 && UIManager.Instance.popUp){
            DialogueManager.Instance.StartConversation("diaryDay4PageKyRoute");
        }
        UIManager.Instance.popUp = false;
        Time.timeScale = 1;
    }

    private void OnEnable() {
        //Show the first page of the diary in a cutscene popup
        if(!UIManager.Instance.popUp)
            GetComponent<Image>().sprite = Diary.diaryPages[0].GetDiaryPage();
        //Show the last page of the diary normally
        else
            GetComponent<Image>().sprite = Diary.diaryPages[Diary.diaryPages.Count - 1].GetDiaryPage();
        Diary.currentPage = 0;
        controls.UI.Enable();
    }

    private void OnDisable() {
        controls.UI.Disable();
    }
}
