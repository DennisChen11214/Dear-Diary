using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages what's shown on the third puzzle on day 4 and the dialogues that play
public class DMDay4Puzzle3 : MonoBehaviour
{
    [SerializeField] GameObject nikko;
    [SerializeField] GameObject kyDiary;
    [SerializeField] GameObject ky;
    [SerializeField] GameObject diaryPages;
    void Awake()
    {
        GameManager.Instance.EnterPuzzle();
        PlayerInventory.SaveInventory();
        Diary.SavePages();
        UIManager.Instance.RemoveEquippedSprite();
        UIManager.Instance.RemoveKeyItemSprite();
        //Only plays the dialogue once, doesn't replay it every time the player dies
        if(!DialogueManager.Instance.puzzleDialogueDone){
            //Depending on if Ky was with Nikko/Simon on previous days, certain dialogues play
            if(!DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
                DialogueManager.Instance.StartConversation("DiaryRouteDay4Puzzle3");
            }
            else if(DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
                DialogueManager.Instance.StartConversation("Day4Puzzle3NeutralNikkoStart");
            }
            else if(!DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2){
                DialogueManager.Instance.StartConversation("Day4Puzzle3NeutralSimonStart");
            }
            else{
                DialogueManager.Instance.StartConversation("Day4Puzzle3KyStart");
            }
            DialogueManager.Instance.puzzleDialogueDone = true;
        }
        //Depending on if Ky was with Nikko/Simon on previous days, we might be able to use Nikko
        if(!DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
            nikko.SetActive(false);
            kyDiary.SetActive(true);
        }
        else if(DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
            ky.SetActive(true);
            diaryPages.SetActive(false);
            ky.GetComponent<KyPowerFire>().TurnOffPowers();
        }
        else if(!DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2){
            nikko.SetActive(false);
            kyDiary.SetActive(true);
        }
        else{
            ky.SetActive(true);
            diaryPages.SetActive(false);
            ky.GetComponent<KyPowerFire>().TurnOffPowers();
        }   
    }
}
