using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue triggered when Ky leaves the dorm building each day
public class BeforeClassDay : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        //Different dialogue each day depending on if Ky was with Nikko/Simon on previous days
        switch(GameManager.Instance.day){
            case 1:
                if(!DecisionManager.Instance.beforeClassDay1){
                    DialogueManager.Instance.StartConversation("ExitDormDay1");
                    DecisionManager.Instance.beforeClassDay1 = true;
                }
                //Dialogue only happens once a day
                gameObject.SetActive(false);
                break;
            case 2:
                if(!DecisionManager.Instance.beforeClassDay2){
                    DialogueManager.Instance.StartConversation("KyExitDormDay2Dialogue");
                    DecisionManager.Instance.beforeClassDay2 = true;
                }
                gameObject.SetActive(false);
                break;
            case 3:
                if(!DecisionManager.Instance.beforeClassDay3){
                    DialogueManager.Instance.StartConversation("KyExitDormDialogue");
                    DecisionManager.Instance.beforeClassDay3 = true;
                }
                gameObject.SetActive(false);
                break;
            case 4:
                if(!DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2 &&
                        !DecisionManager.Instance.beforeClassDay4){
                    DialogueManager.Instance.StartConversation("OutsideDormDay4DiaryRoute");
                    DecisionManager.Instance.beforeClassDay4 = true;
                }
                else if(DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2 &&
                        !DecisionManager.Instance.beforeClassDay4){
                    DialogueManager.Instance.StartConversation("OutsideDormDay4NeutralNikko");
                    DecisionManager.Instance.beforeClassDay4 = true;
                }
                else if(!DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2 &&
                        !DecisionManager.Instance.beforeClassDay4){
                    DialogueManager.Instance.StartConversation("OutsideDormDay4NeutralSimon");
                    DecisionManager.Instance.beforeClassDay4 = true;
                }
                else if(!DecisionManager.Instance.beforeClassDay4){
                    DialogueManager.Instance.StartConversation("OutsideDormBuildingDay4Ky");
                    DecisionManager.Instance.beforeClassDay4 = true;
                }
                gameObject.SetActive(false);
                break;
        }
    }
}
