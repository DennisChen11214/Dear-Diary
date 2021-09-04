using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the dialogue of day 3's puzzle with Nikko
public class DMKyPathFire : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.EnterPuzzle();
        PlayerInventory.SaveInventory();
        Diary.SavePages();
        UIManager.Instance.RemoveEquippedSprite();
        UIManager.Instance.RemoveKeyItemSprite();
        //Only plays the dialogue once, doesn't replay it every time the player dies
        if(!DialogueManager.Instance.puzzleDialogueDone){
            DialogueManager.Instance.StartConversation("PuzzleStartNikkoDialogue");
            DialogueManager.Instance.puzzleDialogueDone = true;
        }
    }

}
