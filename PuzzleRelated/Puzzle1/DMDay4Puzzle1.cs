using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages what's shown on the first puzzle on day 4 and the dialogues that play
public class DMDay4Puzzle1 : MonoBehaviour
{   
    [SerializeField] GameObject nova;
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
                DialogueManager.Instance.StartConversation("DiaryRouteDay4Puzzle1");
            }
            else if(DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
                DialogueManager.Instance.StartConversation("Day4Puzzle1NeutralNikkoStart");
            }
            else if(!DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2){
                DialogueManager.Instance.StartConversation("Day4Puzzle1NeutralSimonStart");
            }
            else{
                DialogueManager.Instance.StartConversation("Day4Puzzle1KyStart");
            }
            DialogueManager.Instance.puzzleDialogueDone = true;
        }   
        //Nova's not here if we weren't with Nikko and Simon previously
        if(!DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
            nova.SetActive(false);
        }
    }
}
