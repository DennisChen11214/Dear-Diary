using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Backdoor to enter a puzzle scene
public class Puzzle1Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Player"){
            return;
        }
        GameManager.Instance.LoadScene("Day4ThirdPuzzle", true, true);
        GameManager.Instance.EnterPuzzle();
    }
}
