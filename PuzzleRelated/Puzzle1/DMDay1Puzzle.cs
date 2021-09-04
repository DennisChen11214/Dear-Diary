using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the dialogue of day 1's puzzle
public class DMDay1Puzzle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.EnterPuzzle();
        PlayerInventory.SaveInventory();
        Diary.SavePages();
        UIManager.Instance.RemoveEquippedSprite();
        UIManager.Instance.RemoveKeyItemSprite();
        //Only plays the dialogue once, doesn't replay it every time the player dies
        if(!DialogueManager.Instance.puzzleDialogueDone){
            DialogueManager.Instance.StartConversation("EnterPuzzleDay1");
            DialogueManager.Instance.puzzleDialogueDone = true;
        }   
    }
}
