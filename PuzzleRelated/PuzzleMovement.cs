using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for the puzzle movement scripts
public class PuzzleMovement : MonoBehaviour
{
    //If the character is moving
    public bool moving{get; protected set;}
    //The direction the character is facing
    public float DirectionFacing{get; protected set;} // -2 = down, -1 = left, 1 = right, 2 = up
    //How much the character moves each step
    public static float stepDistance = 2.0f;
    //Initial position before movement
    protected Vector3 oldPos;
    //Where the character should be after moving
    protected Vector3 newPos;
    protected Vector2 directionDetect; //The player's movement input through the controller
    protected Vector2 directionMoving; //The player's actual movement based on where they're facing
    protected float horizontal;
    protected float vertical;
    protected float direction;
    //Whether the player is facing the same direction as before
    protected bool sameDir = false;
    protected bool animSameDir;
    protected PlayerControls controls;
    protected Animator animator;

    protected virtual void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.Move.performed += ctx => directionDetect = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => directionDetect = Vector2.zero;
        animator = GetComponent<Animator>();
    }

    protected virtual void Start ()
    {
        direction = 0;
        GameManager.onGameStateChanged += GameOver;
        oldPos = transform.position;
    }

    //Destroy the character if we game over
    protected void GameOver(GameManager.GameState previousState, GameManager.GameState currentState){
        if(previousState == GameManager.GameState.PUZZLE && currentState == GameManager.GameState.GAMEOVER){
            Destroy(gameObject);
        }
    }

    //If there is a key in the list of items
    protected bool HasKey(List<ItemInformation> items){
        foreach(ItemInformation item in items){
            if(item.itemType == ItemInformation.ItemType.PuzzleKeyItem){
                return true;
            }
        }
        return false;
    }

    //Remove the key from the list of items
    protected bool RemoveKey(List<ItemInformation> items){
        foreach(ItemInformation item in items){
            if(item.itemType == ItemInformation.ItemType.PuzzleKeyItem){
                PlayerInventory.RemoveItem(item);
                break;
            }
        }
        return false;
    }

    protected void OnEnable() {
        controls.Gameplay.Enable();
    }

    protected void OnDisable() {
        controls.Gameplay.Disable();
        GameManager.onGameStateChanged -= GameOver;
    }

}
