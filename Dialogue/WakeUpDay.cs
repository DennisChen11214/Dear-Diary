using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue triggered upon the start of each day
public class WakeUpDay : MonoBehaviour
{

    void Start()
    {   
        //Destroy the object so that the dialogue isn't triggered more than once a day
        int day = GameManager.Instance.day;
        if( (day == 1 && DecisionManager.Instance.outOfBedDay1) || 
            (day == 2 && DecisionManager.Instance.outOfBedDay2) || 
            (day == 3 && DecisionManager.Instance.outOfBedDay3) || 
            (day == 4 && DecisionManager.Instance.outOfBedDay4)){
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        switch(GameManager.Instance.day){
            case 1:
                DialogueManager.Instance.StartConversation("KyWakeUpDay1");
                DecisionManager.Instance.outOfBedDay1 = true;
                gameObject.SetActive(false);
                break;
            case 2:
                DialogueManager.Instance.StartConversation("KyWakeUpDay2");
                DecisionManager.Instance.outOfBedDay2 = true;
                gameObject.SetActive(false);
                break;
            case 3:
                DialogueManager.Instance.StartConversation("KyWakeUpDorm");
                DecisionManager.Instance.outOfBedDay3 = true;
                gameObject.SetActive(false);
                break;
            //Different dialogue options on day 4 depending on whether we were with Nikko/Simon on the previous days
            case 4:
                if(!DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
                    DialogueManager.Instance.StartConversation("Day4DormDiaryRoute");
                }
                else if(DecisionManager.Instance.withNikkoDay3 && !DecisionManager.Instance.withSimonDay2){
                    DialogueManager.Instance.StartConversation("Day4DormNeutralNikko");
                }
                else if(!DecisionManager.Instance.withNikkoDay3 && DecisionManager.Instance.withSimonDay2){
                    DialogueManager.Instance.StartConversation("Day4DormNeutralSimon");
                }
                else{
                    DialogueManager.Instance.StartConversation("Day4DormKy");
                }
                DecisionManager.Instance.outOfBedDay4 = true;
                gameObject.SetActive(false);
                break;
            case 5:
                DialogueManager.Instance.StartConversation("EpilogueKy");
                break;
        }
    }
}
