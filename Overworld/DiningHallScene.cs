using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the dialogues and material of the objects in the dining hall scene
public class DiningHallScene : MonoBehaviour
{
    //Array of objects outside the dining hall
    [SerializeField] SpriteRenderer[] outsideEnv;
    [SerializeField] Material nightMaterial;
    [SerializeField] GameObject simon;
    [SerializeField] GameObject nikko;

    void Start()
    {
        AudioManager.Instance.PlayBGM(AudioManager.Instance.diningHallBGM);
        //Different dialogues for each day, time of day, and if we're with Simon/Nikko
        if(GameManager.Instance.day == 3 && DecisionManager.Instance.puzzleDoneDay3 && !DecisionManager.Instance.dialogueAfterPuzzle3){
            DialogueManager.Instance.StartConversation("ReenterDiningHallAfterPuzzle");
            DecisionManager.Instance.dialogueAfterPuzzle3 = true;
        }
        else if(GameManager.Instance.day == 1 && !DecisionManager.Instance.diningHallTalkDay1){
            DialogueManager.Instance.StartConversation("DiningHallDay1Friends");
            DecisionManager.Instance.diningHallTalkDay1 = true;
        }
        else if(GameManager.Instance.day == 2 && DecisionManager.Instance.withSimonDay2 && 
                !DecisionManager.Instance.diningHallTalkDay2 && !DecisionManager.Instance.wentToClassDay2){
            DialogueManager.Instance.StartConversation("DiningHallDay2Simon");
            DecisionManager.Instance.diningHallTalkDay2 = true;
        }
        else if(GameManager.Instance.day == 2 && !DecisionManager.Instance.withSimonDay2 &&
                !DecisionManager.Instance.diningHallTalkDay2){
            DialogueManager.Instance.StartConversation("DiningHallMealAloneDay2");
            DecisionManager.Instance.diningHallTalkDay2 = true;
        }
        else if(GameManager.Instance.day == 3 && DecisionManager.Instance.withNikkoDay3 && 
                !DecisionManager.Instance.diningHallTalkDay3 && !DecisionManager.Instance.puzzleDoneDay3){
             DialogueManager.Instance.StartConversation("DiningHallMealNikkoDialogue");
            DecisionManager.Instance.diningHallTalkDay3 = true;
        }
        //Every night, sets the material of all the objects outside the dining hall to be one that can be lit up
        if(GameManager.Instance.day == 1 && DecisionManager.Instance.puzzleDoneDay1){
            foreach(SpriteRenderer spriteRenderer in outsideEnv){
                spriteRenderer.material = nightMaterial;
            }
        }
        else if(GameManager.Instance.day == 2 && DecisionManager.Instance.puzzleDoneDay2){
            foreach(SpriteRenderer spriteRenderer in outsideEnv){
                spriteRenderer.material = nightMaterial;
            }
        }
        else if(GameManager.Instance.day == 3 && DecisionManager.Instance.puzzleDoneDay3){
            foreach(SpriteRenderer spriteRenderer in outsideEnv){
                spriteRenderer.material = nightMaterial;
            }
        }
        //Nikko and Simon only at the dining hall on the first day's afternoon
        else if(GameManager.Instance.day == 1 && !DecisionManager.Instance.puzzleDoneDay1){
            nikko.SetActive(true);
            simon.SetActive(true);
        }
    }

}
