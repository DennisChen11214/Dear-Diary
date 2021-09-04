using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Trigger to change scene to a different part of campus
public class CampusSceneChange : MonoBehaviour
{
    [SerializeField] Vector3 spawnPos;
    [SerializeField] string sceneToLoad;
    //If Ky entered the trigger horizontally or vertically
    [SerializeField] bool horizontal;
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            GameManager.Instance.LoadScene(sceneToLoad, true, true, horizontal, spawnPos.x, spawnPos.y);
        }
    }
}
