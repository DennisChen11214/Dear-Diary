using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the dialogues and material of the objects in the dorm area
public class DormAreaScene : MonoBehaviour
{
    [SerializeField] GameObject beforeClassTrigger;
    [SerializeField] GameObject nova;
    [SerializeField] GameObject simon;
    [SerializeField] GameObject ky;
    [SerializeField] Material nightMaterial;

    private void Start() {
        AudioManager.Instance.PlayBGM(AudioManager.Instance.campusBGM);
        //Different dialogue after the player finishes day 2's puzzle depending on if Simon was there
        if(GameManager.Instance.day == 2 && DecisionManager.Instance.puzzleDoneDay2 && 
           !DecisionManager.Instance.dialogueAfterPuzzle2 && DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("Day2NightSimonRoute");
            DecisionManager.Instance.dialogueAfterPuzzle2 = true;
        }
        else if(GameManager.Instance.day == 2 && DecisionManager.Instance.puzzleDoneDay2 && 
           !DecisionManager.Instance.dialogueAfterPuzzle2 && !DecisionManager.Instance.withSimonDay2){
            DialogueManager.Instance.StartConversation("Day2NightAlone");
            DecisionManager.Instance.dialogueAfterPuzzle2 = true;
        }
        //Have Nova follow Ky on day 1
        if(GameManager.Instance.day == 1 && !DecisionManager.Instance.beforeClassDay1){
            nova.SetActive(true);
            nova.GetComponent<FollowKy>().SetTarget(ky);
        }
        //Have Simon follow Ky on day 2
        else if(GameManager.Instance.day == 2 && !DecisionManager.Instance.beforeClassDay2){
            simon.SetActive(true);
            simon.GetComponent<FollowKy>().SetTarget(ky);
        }
        //Nova and simon not spawned on days 3 and 4
        else{
            nova.SetActive(false);
            simon.SetActive(false);
        }
        //Every night, sets the material of all the objects to be one that can be lit up
        if(GameManager.Instance.day == 1 && DecisionManager.Instance.puzzleDoneDay1){
            SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
            foreach(SpriteRenderer spriteRenderer in allSprites){
                spriteRenderer.material = nightMaterial;
            }
        }
        if(GameManager.Instance.day == 2 && DecisionManager.Instance.puzzleDoneDay2){
            SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
            foreach(SpriteRenderer spriteRenderer in allSprites){
                spriteRenderer.material = nightMaterial;
            }
        }
        if(GameManager.Instance.day == 3 && DecisionManager.Instance.puzzleDoneDay3){
            SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
            foreach(SpriteRenderer spriteRenderer in allSprites){
                spriteRenderer.material = nightMaterial;
            }
        }
    }
}
