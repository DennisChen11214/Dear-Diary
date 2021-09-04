using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{ 
    //Add an onclick event to the button on this object
    private void Start() {
        GetComponent<Button>().onClick.AddListener(delegate {Restart();});
    }

    //Restarts the level in a puzzle by reverting everything to the state before the puzzle
    void Restart(){
        PlayerInventory.Revert();
        Diary.RevertPages();
        string sName = SceneManager.GetActiveScene().name;
        UIManager.Instance.RemoveEquippedSprite();
        UIManager.Instance.RemoveKeyItemSprite();   
        GameManager.Instance.LoadScene(sName, true, true);
        GameManager.Instance.EnterPuzzle();
    }
}
