using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls who's shown on the rooftop and which dialogue is played
public class Rooftop : MonoBehaviour
{
    [SerializeField] GameObject nikko;
    [SerializeField] GameObject simon;
    [SerializeField] GameObject ayana;
    [SerializeField] GameObject nova;
    void Start()
    {
        GameManager.Instance.StartGame();
        //Only Ky is on the rooftop - bad ending
        if(!DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("DiaryRouteEnding");
            nikko.SetActive(false);
            simon.SetActive(false);
            ayana.SetActive(false);
            nova.SetActive(false);
            //Shown one final diary page
            ItemInformation diaryEndPage = new ItemInformation();
            diaryEndPage.itemType = ItemInformation.ItemType.Diary;
            diaryEndPage.day = 4;
            diaryEndPage.end = true;
            Diary.AddPage(diaryEndPage);
        }
        //Nikko, Nova, Ky, and Ayana are shown - neutral ending
        else if(DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("NeutralNikkoEnding");
            simon.SetActive(false);
        }
        //Simon, Nova, Ky, and Ayana are shown - neutral ending
        else if(!DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("NeutralSimonEnd");
            nikko.SetActive(false);
        }
        //Everyone's shown - good ending
        else{
            DialogueManager.Instance.StartConversation("KyEnding");
        }
    }
}
