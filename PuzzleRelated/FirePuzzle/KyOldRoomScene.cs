using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the dialogue and who shows up in the room after day 3's puzzle
public class KyOldRoomScene : MonoBehaviour
{
    [SerializeField] GameObject nikko;
    [SerializeField] GameObject simon;

    void Start()
    {
        StartDialogue();
        //Simon is here if we were with him on day 2
        if(DecisionManager.Instance.withSimonDay2)
            simon.SetActive(true);
        //Nikko is here if we are with him on day 3
        if(DecisionManager.Instance.withNikkoDay3)
            nikko.SetActive(true);
    }

    void StartDialogue(){
        //Depending on if Ky was with Nikko/Simon on previous days, certain dialogues play
        if(DecisionManager.Instance.withSimonDay2 && !DecisionManager.Instance.withNikkoDay3){
            DialogueManager.Instance.StartConversation("PuzzleEndSimonDialogue");
        }
        else if(!DecisionManager.Instance.withSimonDay2 && !DecisionManager.Instance.withNikkoDay3){
            DialogueManager.Instance.StartConversation("PuzzleEndAloneDialogue");
        }
        else if(DecisionManager.Instance.withSimonDay2 && DecisionManager.Instance.withNikkoDay3){
            DialogueManager.Instance.StartConversation("PuzzleEndNikkoSimonDialogue");
        }
        else{
            DialogueManager.Instance.StartConversation("PuzzleEndNikkoDialogue");
        }
        //We gain a diary page
        ItemInformation diaryEndPage = new ItemInformation();
        diaryEndPage.itemType = ItemInformation.ItemType.Diary;
        diaryEndPage.day = 3;
        diaryEndPage.end = true;
        Diary.AddPage(diaryEndPage);
    }

}
