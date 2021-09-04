using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Used to generalize character powers so that each character's power can be accessed in PlayerMovement
*/
public class Power : MonoBehaviour
{
    //Whose power is this
    public enum Person{
        KY,
        NIKKO,
        NOVA,
        SIMON
    }
    //Whether this character can use their powers
    protected bool canUsePowers = true;
    //Reference to the movement script
    [SerializeField] protected PuzzleMovement movement;

    public Person charName {get; protected set;}

    protected PlayerControls controls;

    public void TurnOffPowers(){
        canUsePowers = false;
    }

    protected void OnEnable() {
        controls.Gameplay.Enable();
    }

    protected void OnDisable() {
        controls.Gameplay.Disable();
    }
}
