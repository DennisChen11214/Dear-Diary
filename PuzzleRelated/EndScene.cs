using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controls which sprite is shown for the end scene
public class EndScene : MonoBehaviour
{
    [SerializeField] Sprite badEnding;
    [SerializeField] Sprite neutralEnding;
    [SerializeField] Sprite goodEnding;

    private void Start() {
        GameManager.Instance.GoToMenu();
        //3 Different endings depending if Ky was with Simon/Nikko on previous days
        if(!DecisionManager.Instance.withSimonDay2 && !DecisionManager.Instance.withNikkoDay3)
            GetComponent<Image>().sprite = badEnding;
        else if(DecisionManager.Instance.withSimonDay2 && DecisionManager.Instance.withNikkoDay3)
            GetComponent<Image>().sprite = goodEnding;
        else
            GetComponent<Image>().sprite = neutralEnding;
        //Move to the main menu after 5 seconds
        Invoke("GoToMainMenu", 5f);
    }

    void GoToMainMenu(){
        GameManager.Instance.LoadScene("Main Menu", true, true);
    }
}
