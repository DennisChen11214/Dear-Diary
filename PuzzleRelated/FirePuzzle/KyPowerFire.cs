using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyPowerFire : Power
{
    #region Public Variables
    public delegate void PickUpItem(ItemInformation item);
    //Event for what happens when we pick up an item
    public static event PickUpItem onItemPicked;
    #endregion
    #region Serialized Fields
    [SerializeField] GameObject boxCreated;
    #endregion
    #region Private Variables
    //Size of the box we create
    Vector3 dimensions;
    //Original position of the diary page we pick up that it returns to if we pick up another page
    static Vector3 originalItemPos;
    GameObject itemDetect;
    static GameObject itemHolder;
    bool inItem = false;
    static bool hasCreatePage = false;
    static bool hasDestroyPage = false;
    [SerializeField] int createMax;
    static int created;
    [SerializeField] int destroyMax;
    //On day 1, only Nova can use the powers
    [SerializeField] bool day1Ky;
    static int destroyed;
    #endregion

    private void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.CreateDestroy.performed += ctx => CreateDestroy();
        controls.Gameplay.PickUp.performed += ctx => PickUp();
        created = 0;
        destroyed = 0;
        hasCreatePage = false;
        hasDestroyPage = false;
        itemHolder = null;
        originalItemPos = Vector3.zero;
        dimensions = new Vector2(GetComponent<BoxCollider2D>().size.x * transform.localScale.x, GetComponent<BoxCollider2D>().size.y * transform.localScale.y) ;
    }

    //Picks up an item if on top of one
    void PickUp(){
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        //If we're on top of an item, the person in front, not moving, and not Ky on day 1
        if(inItem && movement && !movement.moving && !day1Ky){
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

    void CreateDestroy(){
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        if(movement && movement.moving || GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE || !canUsePowers || day1Ky)
            return;
        //If we're holding onto a create page and have enough creations left
        if(hasCreatePage && created < createMax){
            float direction = movement.DirectionFacing;
            Vector2 dir = new Vector2(0,0);
            if(direction == -1 || direction == 1){
                dir = new Vector2(direction,0);
            }
            else{
                dir = new Vector2(0,direction / 2);
            }
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, dir, dimensions.x * 2);
            //Creates a tile in the direction the player is facing if there's a hole there
            if(hit2D && hit2D.collider.gameObject.tag == "Hole"){
                hit2D.collider.gameObject.GetComponent<Collider2D>().enabled = false;
                Vector3 tilePos = hit2D.collider.gameObject.transform.position - new Vector3(0,0,1);
                GameObject box = Instantiate(boxCreated, tilePos, Quaternion.identity);
                box.GetComponent<Collider2D>().enabled = false;
                box.GetComponent<Animator>().Play("BlockFallInHole");
                created++;
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
    }

    private void OnTriggerExit2D(Collider2D other) {
        //Set that we're no longer on an item
        if(other.gameObject.tag.Equals("Item")){
           inItem = false;
           itemDetect = null;
        }
    }
}
