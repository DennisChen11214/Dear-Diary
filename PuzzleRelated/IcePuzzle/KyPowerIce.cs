using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyPowerIce : Power
{
    #region Public Variables
    public delegate void PickUpItem(ItemInformation item);
    //Event for what happens when we pick up an item
    public static event PickUpItem onItemPicked;
    public delegate void ButtonPressed();
    //Event for what happens when we step on a button
    public static event ButtonPressed onButtonPressed;
    #endregion
    #region Serialized Fields
    [SerializeField] GameObject boxCreated;
    //[SerializeField] UI_Inventory uiInventory;
    #endregion
    #region Private Variables
    //Size of the box we create
    Vector3 dimensions;
    //Original position of the diary page we pick up that it returns to if we pick up another page
    Vector3 originalItemPos;
    int puzzleNumber;
    GameObject itemDetect;
    GameObject itemHolder;
    bool inItem = false;
    bool hasCreatePage = false;
    bool hasDestroyPage = false;
    bool onButton = false;
    [SerializeField] int createMax;
    int created = 0;
    [SerializeField] int destroyMax;
    int destroyed = 0;
    #endregion

    private void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.CreateDestroy.performed += ctx => CreateDestroy();
        controls.Gameplay.PickUp.performed += ctx => PickUp();
        controls.Gameplay.StayOnButton.performed += ctx => StayOnButton();
    }

    private void Start() {
        //Gets the dimensions of the player and box
        dimensions = new Vector2(GetComponent<BoxCollider2D>().size.x * transform.localScale.x, GetComponent<BoxCollider2D>().size.y * transform.localScale.y) ;
    }

    //Picks up an item if on top of one
    void PickUp(){
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        //If we're on top of an item, the person in front and not moving
        if(inItem && movement && !movement.moving){
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.keyItem);
            //Put the item in the inventory, call the event, and add the page to the diary if it is a page
            Item pickedUp = itemDetect.gameObject.GetComponent<Item>();
            if(pickedUp.itemInformation.itemType == ItemInformation.ItemType.PuzzleKeyItem)
                PlayerInventory.AddItem(pickedUp.itemInformation);
            onItemPicked(pickedUp.itemInformation);
            Diary.AddPage(pickedUp.itemInformation);
            //If we picked up a diary page that lets us create blocks
            if(pickedUp.itemInformation.itemType == ItemInformation.ItemType.PuzzleCreatePage){
                UIManager.Instance.SetNumPages((createMax - created).ToString());
                hasCreatePage = true;
                hasDestroyPage = false;
                //If we're currently holding onto a page, put it back to its original position
                if(itemHolder != null){
                    PlayerInventory.RemoveItem(itemHolder.GetComponent<Item>().itemInformation);
                    itemHolder.SetActive(true);
                    itemHolder.transform.position = originalItemPos;
                }
                itemHolder = itemDetect;
                originalItemPos = transform.position;
            }
            //If we picked up a diary page that lets us destroy blocks
            else if(pickedUp.itemInformation.itemType == ItemInformation.ItemType.PuzzleDestroyPage){
                UIManager.Instance.SetNumPages((destroyMax - destroyed).ToString());
                hasCreatePage = false;
                hasDestroyPage = true;
                //If we're currently holding onto a page, put it back to its original position
                if(itemHolder != null){
                    PlayerInventory.RemoveItem(itemHolder.GetComponent<Item>().itemInformation);
                    itemHolder.SetActive(true);
                    itemHolder.transform.position = originalItemPos;
                }
                itemHolder = itemDetect;
                originalItemPos = transform.position;
            }
            inItem = false;
            itemDetect.SetActive(false);
        }
    }

    //If we step on a button and stay there, swap control off this character
    void StayOnButton(){
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        if(onButton && !movement.moving){
            onButtonPressed();
            movement.enabled = false;
        }
    }

    void CreateDestroy(){
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        if(movement && movement.moving || GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE || !canUsePowers)
            return;
        //If we're holding onto a create page and have enough creations left
        if(hasCreatePage && created < createMax){
            float direction = movement.DirectionFacing;
            Vector3 boxPos = new Vector3(0,0,0);
            if(direction == -1 || direction == 1){
                boxPos = new Vector3(direction,0,0);
            }
            else{
                boxPos = new Vector3(0,direction,0);
            }
            RaycastHit2D hit2D;
            hit2D = Physics2D.Raycast(transform.position, boxPos, PuzzleMovement.stepDistance, 1 << 9);
            //Creates a tile in the direction the player is facing if there's a hole there
            if(hit2D && hit2D.collider.gameObject.tag == "Hole"){
                hit2D.collider.gameObject.GetComponent<Collider2D>().enabled = false;
                Vector3 tilePos = hit2D.collider.gameObject.transform.position - new Vector3(0,0,1);
                GameObject box = Instantiate(boxCreated, tilePos, Quaternion.identity);
                box.GetComponent<Collider2D>().enabled = false;
                created++;
                box.GetComponent<Animator>().Play("BlockFallInHole");
                UIManager.Instance.SetNumPages((createMax - created).ToString());
            }

        }
        //If we're holding onto a destroy page and have enough destructions left
        else if(hasDestroyPage && destroyed < destroyMax){
            float direction = movement.DirectionFacing;
            Vector2 dir = new Vector2(0,0);
            if(direction == -1 || direction == 1){
                dir = new Vector2(direction,0);
            }
            else{
                dir = new Vector2(0,direction / 2);
            }
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, dir, dimensions.x * 2);
            //Destroys a box in the direction the player is facing if there is one
            if(hit2D && hit2D.collider.gameObject.tag == "Moveable"){
                Destroy(hit2D.collider.gameObject);
                destroyed++;
                UIManager.Instance.SetNumPages((destroyMax - destroyed).ToString());
            }
        }
    } 

    private void OnTriggerEnter2D(Collider2D other) {
        //Set that we're on an item
        if(other.gameObject.tag.Equals("Item")){
           inItem = true;
           itemDetect = other.gameObject;
        }
        //Set that we're on a button
        if(other.gameObject.tag.Equals("Button")){
            onButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        //Set that we're no longer on an item
        if(other.gameObject.tag.Equals("Item")){
           inItem = false;
           itemDetect = null;
        }
        //Set that we're no longer on a button
        if(other.gameObject.tag.Equals("Button")){
            onButton = false;
        }
    }
}
