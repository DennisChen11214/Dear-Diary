using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controls the dialogues and material of the objects in the lecture hall scene
public class LectureAreaScene : MonoBehaviour
{
    [SerializeField] GameObject nikko;
    [SerializeField] GameObject nikkoFollow;
    [SerializeField] GameObject ky;
    [SerializeField] GameObject cam;
    //Position at the front of the lecture hall
    [SerializeField] Vector3 lectureFront;
    [SerializeField] Material nightMaterial;

    private void Start() {
        //Plays the fade after talking to Nikko on day 3
        DialogueManager.onFollowNikko4 += Fade;
        //Dialogue after finishing the puzzle on the first day
        if(GameManager.Instance.day == 1 && DecisionManager.Instance.puzzleDoneDay1 && !DecisionManager.Instance.dialogueAfterPuzzle1){
            DialogueManager.Instance.StartConversation("Day1AfterPuzzleMonologue");
            DecisionManager.Instance.dialogueAfterPuzzle1 = true;
        }
        //Every night, sets the material of all the objects in the scene to be one that can be lit up
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

    //Screen fades to black and fades back in to indicate time passing
    void Fade(){
        StartCoroutine("FadeInOut");
        UIManager.Instance.FadeIn();
    }

    IEnumerator FadeInOut(){
        float aTime = 2.0f; 
        //So the player doesn't talk to Nikko again
        nikko.GetComponent<Collider2D>().enabled = false;
        //Waits for the black screen to fade in
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            yield return null;
        }
        //Start fading back to normal
        UIManager.Instance.FadeOut();
        //Move Ky and have him face the right direction
        ky.transform.position = lectureFront;
        ky.GetComponent<Animator>().Play("IdleFront");
        //Move the camera
        cam.transform.position = lectureFront + new Vector3(0,0,-10);
        //Different dialogue for day 2 and 3
        if(GameManager.Instance.day == 2){
            DialogueManager.Instance.StartConversation("PostLectureSimonDialogue");
            GameObject simon = GameObject.FindGameObjectWithTag("Simon");
            simon.transform.position = ky.transform.position + new Vector3(0,1,1);
            simon.GetComponent<Animator>().Play("IdleFront");
            simon.GetComponent<FollowKy>().SetTarget(ky);
        }
        else{
            DialogueManager.Instance.StartConversation("NikkoAfterLectureDialogue");
            nikko.SetActive(false);
            //Nikko starts following behind Ky
            nikkoFollow.SetActive(true);
            nikkoFollow.transform.position = ky.transform.position + new Vector3(0,1,1);
            nikkoFollow.GetComponent<Animator>().Play("IdleFront");
            nikkoFollow.GetComponent<FollowKy>().SetTarget(ky);
        }
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            yield return null;
        }
    }

    private void OnDestroy() {
        DialogueManager.onFollowNikko4 -= Fade;
    }
}
