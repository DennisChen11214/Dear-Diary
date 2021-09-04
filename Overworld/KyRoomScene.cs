using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Different dialogues played at the end of each day when Ky comes back to his room after finishing the puzzles
public class KyRoomScene : MonoBehaviour
{
    void Start()
    {
        if(DecisionManager.Instance.puzzleDoneDay1 && GameManager.Instance.day == 1){
            DialogueManager.Instance.StartConversation("Day1EndDormDialogue");   
            KyMovementCity.Face(-2);
        }
        if(DecisionManager.Instance.puzzleDoneDay2 && GameManager.Instance.day == 2 &&
           DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("Day2NightDormSimonRoute");   
            KyMovementCity.Face(-2);
        }
        if(DecisionManager.Instance.puzzleDoneDay2 && GameManager.Instance.day == 2 &&
           !DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("Day2NightDormAlone");   
            KyMovementCity.Face(-2);
        }
        if(DecisionManager.Instance.puzzleDoneDay3 && GameManager.Instance.day == 3){
            DialogueManager.Instance.StartConversation("ReenterDormAfterPuzzle");   
            KyMovementCity.Face(-2);
        }
    }
}
