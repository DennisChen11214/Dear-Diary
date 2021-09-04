using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages what's shown on the second puzzle on day 4 and the dialogues that play
public class DMDay4Puzzle2 : MonoBehaviour
{
    //Different obstacles if Simon is with us
    [SerializeField] List<GameObject> kyRouteObjects;
    [SerializeField] List<GameObject> diaryRouteObjects;
    [SerializeField] GameObject simon;
    [SerializeField] GameObject kyDiary;
    [SerializeField] GameObject ky;

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
                DialogueManager.Instance.StartConversation("DiaryRouteDay4Puzzle2");
            }
            else if(DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
                DialogueManager.Instance.StartConversation("Day4Puzzle2NeutralNikkoStart");
            }
            else if(!DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2){
                DialogueManager.Instance.StartConversation("Day4Puzzle2NeutralSimonStart");
            }
            else{
                DialogueManager.Instance.StartConversation("Day4Puzzle2KyStart");
            }
            DialogueManager.Instance.puzzleDialogueDone = true;
        } 
        //Depending on if Ky was with Nikko/Simon on previous days, we might be able to use Simon
        if(!DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
            simon.SetActive(false);
            foreach(GameObject obj in kyRouteObjects){
                obj.SetActive(false);
            }
            kyDiary.SetActive(true);
        }
        else if(DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
            simon.SetActive(false);
            foreach(GameObject obj in kyRouteObjects){
                obj.SetActive(false);
            }
            kyDiary.SetActive(true);
        }
        else if(!DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2){
            foreach(GameObject obj in diaryRouteObjects){
                obj.SetActive(false);
            }
            ky.SetActive(true);
            ky.GetComponent<KyPowerIce>().TurnOffPowers();
        }
        else{
            foreach(GameObject obj in diaryRouteObjects){
                obj.SetActive(false);
            }
            ky.SetActive(true);
            ky.GetComponent<KyPowerIce>().TurnOffPowers();
        }
    }
}
