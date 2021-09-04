using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class for displaying Ky's diary pages on the screen
public class Diary : MonoBehaviour
{
    //UI object that'll have the diary page image
    [SerializeField] GameObject diaryPage;
    PlayerControls controls;
    //Stores if the page is flipped to the left/right
    float flipSide;
    //List of the diary pages Ky possesses
    public static List<ItemInformation> diaryPages;
    //Diary pages are saved at the start of a puzzle to be loaded in again on a game over or restart
    public static List<ItemInformation> savedPages;
    public static int currentPage;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.UI.BrowseDiary.performed += ctx => flipSide = ctx.ReadValue<float>();
        controls.UI.BrowseDiary.performed += ctx => ChangePage(flipSide);
        diaryPages = new List<ItemInformation>();
        savedPages = new List<ItemInformation>();
    }

    //Adds a page to the diary
    public static void AddPage(ItemInformation page){
        //Don't add in the key item
        if(page.itemType == ItemInformation.ItemType.PuzzleKeyItem)
            return;
        bool inDiary = false;
        //Don't add the page if we already have it
        foreach(ItemInformation item in diaryPages){
            if(page.GetDiaryPage().name == item.GetDiaryPage().name){
                inDiary = true;
            }
        }
        if(!inDiary){
            diaryPages.Add(page);
        }
    }

    //Changes the page the player is viewing a page back or forward
    void ChangePage(float side){
        //Only change pages if the diary is active on the screen and it isn't just a cutscene popup
        if(UIManager.Instance.popUp || !diaryPage.activeSelf || 
                GameManager.Instance.CurrentGameState == GameManager.GameState.PAUSED)
            return;
        //Move back a page
        if(side == -1 && currentPage >= 1){
            currentPage--;
            diaryPage.GetComponent<Image>().sprite = diaryPages[currentPage].GetDiaryPage();
        }
        //Move forward a page
        else if(side == 1 && currentPage <= diaryPages.Count - 2){
            currentPage++;
            diaryPage.GetComponent<Image>().sprite = diaryPages[currentPage].GetDiaryPage();
        }
    }

    public static void ClearDiary(){
        diaryPages.Clear();
    }

    //Saves the pages at the beginning of a puzzle
    public static void SavePages(){
        savedPages = new List<ItemInformation>();
        foreach(ItemInformation itemInformation in diaryPages){
            savedPages.Add(itemInformation);
        }
    }

    //Loads in the saved pages at a game over or restart
    public static void RevertPages(){
        diaryPages = new List<ItemInformation>();
        foreach(ItemInformation itemInformation in savedPages){
            diaryPages.Add(itemInformation);
        }
    }

    private void OnEnable() {
        controls.UI.Enable();
    }

    private void OnDisable() {
        controls.UI.Disable();
    }

}
