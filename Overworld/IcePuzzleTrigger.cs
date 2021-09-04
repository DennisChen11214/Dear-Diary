using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Used as a backdoor to go into the puzzle
public class IcePuzzleTrigger : MonoBehaviour
{
    public bool diaryPath = true;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Player"){
            return;
        }
        if(diaryPath){
            GameManager.Instance.LoadScene("Day4SecondPuzzle", true, true);
        }
        else{
            GameManager.Instance.LoadScene("KyPathIce", true, true);
        }
        GameManager.Instance.EnterPuzzle();
    }
}
