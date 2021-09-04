using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonPower : Power
{
    #region Public Variables
    public delegate void FlipSwitch();
    //Event that the movement subscribes to, activated when Simon uses the switch
    public static event FlipSwitch onSwitchFlipped;
    #endregion
    #region Serialized Fields
    #endregion
    #region Private Variables
    #endregion

    private void Awake() {
        controls = new PlayerControls();
        charName = Power.Person.SIMON;
        controls.Gameplay.PushBox.performed += ctx => Flip();
    }   

    //Sends a signal to PlayerMovement to flip the switch if possible
    void Flip(){
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        onSwitchFlipped();
    }

}
