using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTrigger : MonoBehaviour
{
    //Changes gamestate at the beginning of the scene
    private void Start() {
        GameManager.Instance.GoToMenu();
    }
}
