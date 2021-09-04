using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NikkoPower : Power
{
    #region Public Variables
    public delegate void MoveWhilePushing();
    //Event that the movement subscribes to, activated when Nikko tries to push a box
    public static event MoveWhilePushing movePush;
    #endregion
    #region Serialized Fields
    #endregion
    #region Private Variables
    int puzzleNumber;
    #endregion

    private void Awake() {
        controls = new PlayerControls();
        charName = Power.Person.NIKKO;
        controls.Gameplay.PushBox.performed += ctx => PushBox();
    }   

    //Sends a signal to PlayerMovement to move and push a box if possible
    void PushBox(){
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        movePush();
    }

}
