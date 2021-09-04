using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Dialogue triggered by interacting with the door to Ky's room if the diary hasn't been checked that day
public class KyRoomDoor : MonoBehaviour
{
    [SerializeField] Vector3 spawnPos;
    [SerializeField] string sceneToLoad;
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            if((GameManager.Instance.day == 1 && !DecisionManager.Instance.diaryCheckedDay1) ||
               (GameManager.Instance.day == 2 && !DecisionManager.Instance.diaryCheckedDay2) ||
               (GameManager.Instance.day == 3 && !DecisionManager.Instance.diaryCheckedDay3)){
                DialogueManager.Instance.StartConversation("KyDormDoorBeforeDiary");
            }
            else if(GameManager.Instance.day == 4 && !DecisionManager.Instance.diaryCheckedDay4){
                DialogueManager.Instance.StartConversation("Day4LeaveDormEarly");
            }
            else{
                GameManager.Instance.LoadScene(sceneToLoad, true, true, false);
            }
        }
    }

}
