using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    //All the UI elements that show up on the screen
    [SerializeField] GameObject gameOverText; 
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject saveButton;
    [SerializeField] GameObject equippedItems;
    [SerializeField] GameObject item1;
    [SerializeField] GameObject item2;
    [SerializeField] GameObject fade;
    [SerializeField] GameObject diaryPage;
    [SerializeField] GameObject lunchImage;
    [SerializeField] Sprite day1Eating;
    [SerializeField] Sprite day2Eating;
    [SerializeField] Sprite day3Eating;

    bool IsPaused = false;
    bool inPuzzle;
    public bool popUp;
    //Gamestate before pausing the game
    public GameManager.GameState beforePause{get; private set;}
    //Gamestate before opening the diary
    GameManager.GameState beforeDiary = GameManager.GameState.TOWN;
    PlayerControls controls;

    protected override void Awake()
    {
        base.Awake();
        controls = new PlayerControls();
        controls.UI.Pause.performed += ctx => TogglePause();
        controls.UI.Inventory.performed += ctx => ToggleDiary();
    }

    private void Start() {
        DontDestroyOnLoad(gameObject);
        GameManager.onGameStateChanged += GameOver;
        GameManager.onGameStateChanged += SetUpPuzzle;
        GameManager.onGameStateChanged += Paused;
        GameManager.onGameStateChanged += Inventory;
        GameManager.onGameStateChanged += EquippedItems;
        GameManager.onGameStateChanged += RestartToggle;
        KyPowerFire.onItemPicked += SetEquippedSprite;
        KyPowerIce.onItemPicked += SetEquippedSprite;
    }

    public void TogglePause(){
        //Make sure the game should be able to be paused
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.MENU || 
           GameManager.Instance.CurrentGameState == GameManager.GameState.GAMEOVER ||
           GameManager.Instance.fadingIn || GameManager.Instance.fadingOut || popUp ||
           GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE ||
           GameManager.Instance.CurrentGameState == GameManager.GameState.INVENTORY){
            return;
        }
        if (IsPaused)
        {
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.buttonPress);
            //Switch back to the gamestate before pausing
            switch(beforePause){
            case GameManager.GameState.TOWN:
                GameManager.Instance.StartGame();
                break;
            case GameManager.GameState.PUZZLE:
                GameManager.Instance.EnterPuzzle();
                break;
            case GameManager.GameState.DIALOGUE:
                GameManager.Instance.EnterDialogue();
                break;
            case GameManager.GameState.INVENTORY:
                GameManager.Instance.EnterInventory();
                break;
            default:
                break;
            }
        }
        else
        {
            beforePause = GameManager.Instance.CurrentGameState;
            GameManager.Instance.Pause();
        }
    }

    public void ToggleDiary(){
        //Make sure the diary should be able to be opened
        if((GameManager.Instance.CurrentGameState != GameManager.GameState.TOWN && 
                GameManager.Instance.CurrentGameState != GameManager.GameState.PUZZLE &&
                GameManager.Instance.CurrentGameState != GameManager.GameState.INVENTORY) ||
                GameManager.Instance.fadingIn || popUp || GameManager.Instance.fadingOut ||
                !DecisionManager.Instance.diaryCheckedDay1){
            return;
        }
        //Save the current state if the diary is not currently active
        if(!diaryPage.activeSelf){
            beforeDiary = GameManager.Instance.CurrentGameState;
            GameManager.Instance.EnterInventory();
        }
        //Switch back to the state before opening the diary
        else if(beforeDiary == GameManager.GameState.TOWN)
            GameManager.Instance.StartGame();
        else if(beforeDiary == GameManager.GameState.PUZZLE)
            GameManager.Instance.EnterPuzzle();
    }

    //Load in the main menu scene after hitting the button in the pause menu
    public void LoadMain()
    {
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.buttonPress);
        pauseMenu.SetActive(false);
        GameManager.Instance.LoadScene("Main Menu");
    }

    //Save the game after hitting the save button
    public void Save(){
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.buttonPress);
        GameManager.Instance.SaveGame();
    }

    //Load the game after hitting the load button on the main menu
    public void Load(){
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.buttonPress);
        GameManager.Instance.LoadGame();
    }

    //Restart the puzzle after hitting the restart button on the pause menu
    public void Restart(){
        PlayerInventory.Revert();
        Diary.RevertPages();
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.buttonPress);
        string sName = SceneManager.GetActiveScene().name;
        GameManager.Instance.LoadScene(sName, false, true);
        GameManager.Instance.EnterPuzzle();
    }

    //Screen stops its current fade and starts a new one
    public void FadeIn(){
        StopCoroutine("FadeInBlack");
        StartCoroutine("FadeInBlack");   
    }
    public void FadeOut(){
        StopCoroutine("FadeOutBlack");
        StartCoroutine("FadeOutBlack");
    }

    //Screen also fades when Ky is sleeping
    public void Sleep(){
        StartCoroutine("Sleeping");
    }

    //Image of Ky and his friends eating shows up on screen
    public void Eat(){
        GameManager.Instance.EnterDialogue();
        switch(GameManager.Instance.day){
            case 1:
                lunchImage.GetComponent<Image>().sprite = day1Eating;
                break;
            case 2:
                lunchImage.GetComponent<Image>().sprite = day2Eating;
                break;
            case 3:
                lunchImage.GetComponent<Image>().sprite = day3Eating;
                break;
        }
        StartCoroutine("Eating");
    }

    IEnumerator Eating(){
        float aTime = 1.0f;
        //Fade in the image of everyone eating
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = lunchImage.GetComponent<Image>().color;
            newColor = new Color(newColor.r, newColor.g, newColor.b, Mathf.Lerp(0,1,t));
            lunchImage.GetComponent<Image>().color = newColor;
            yield return null;
        }
        yield return new WaitForSeconds(2);
        //Fade back out to the game scene
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = lunchImage.GetComponent<Image>().color;
            newColor = new Color(newColor.r, newColor.g, newColor.b, Mathf.Lerp(1,0,t));
            lunchImage.GetComponent<Image>().color = newColor;
            yield return null;
        }
        //Start a dialogue after the image finished fading out
        switch(GameManager.Instance.day){
            case 1:
                DialogueManager.Instance.StartConversation("DiningHallDay1FriendsAfter");
                break;
            case 2:
                DialogueManager.Instance.StartConversation("DiningHallDay2SimonYesAfter");
                break;
            case 3:
                DialogueManager.Instance.StartConversation("MealNikkoChooseYesAfter");
                break;
        }
    }

    IEnumerator Sleeping(){
        GameManager.Instance.fadingIn = true;
        GameManager.Instance.fadingOut = true;
        float aTime = 1.0f;
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.sleep);
        //Screen fades to black after sleeping
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(0,1,t));
            fade.GetComponent<Image>().color = newColor;
            yield return null;
        }
        yield return new WaitForSeconds(2);
        GameManager.Instance.fadingIn = false;
        //Ky's room is reloaded and we fade back to the game
        GameManager.Instance.LoadScene("KyRoom");
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(1,0,t));
            fade.GetComponent<Image>().color = newColor;
            yield return null;
        }
        GameManager.Instance.fadingOut = false;
    }

    //Used for toggling the popup diary page
    public void ToggleDiaryPage(){
        if(!diaryPage.activeSelf){
            beforeDiary = GameManager.Instance.CurrentGameState;
            GameManager.Instance.EnterInventory();
        }
        else if(beforeDiary == GameManager.GameState.TOWN)
            GameManager.Instance.StartGame();
        else if(beforeDiary == GameManager.GameState.PUZZLE)
            GameManager.Instance.EnterPuzzle();
    }

    //Screen fades to black
    IEnumerator FadeInBlack(){
        float aTime = 1.0f;
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.sceneTransition);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(0,1,t));
            fade.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }

    //Screen fades back to the game
    IEnumerator FadeOutBlack(){
        float aTime = 1.0f;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(1,0,t));
            fade.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }

    //Set the game over UI to active if we're currently in the GAMEOVER gamestate
    void GameOver(GameManager.GameState previousState, GameManager.GameState currentState){
        if(previousState == GameManager.GameState.PUZZLE && currentState == GameManager.GameState.GAMEOVER){
            gameOverText.SetActive(true);
        }
        else{
            gameOverText.SetActive(false);
        }
    }

    void SetUpPuzzle(GameManager.GameState previousState, GameManager.GameState currentState){
        if(currentState == GameManager.GameState.PUZZLE){
            inPuzzle = true;
        }
    }

    void Paused(GameManager.GameState previousState, GameManager.GameState currentState){
        if(currentState == GameManager.GameState.PAUSED){
            //Activate the pause menu and set the time scale to 0
            pauseMenu.SetActive(true);
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.menu);
            pauseMenu.GetComponent<PauseMenuUI>().enabled = true;
            Time.timeScale = 0f;
            IsPaused = true;
            //Only have the restart button if we're in a puzzle and save button if we're not in a puzzle
            if(!inPuzzle){
                restartButton.SetActive(false);
                saveButton.SetActive(true);
            }
            else{
                restartButton.SetActive(true);
                saveButton.SetActive(false);
            }
        }
        else{
            //Turn off the pause menu and set the time scale back to normal
            pauseMenu.SetActive(false);
            pauseMenu.GetComponent<PauseMenuUI>().enabled = false;
            if(currentState != GameManager.GameState.INVENTORY)
                Time.timeScale = 1f;
            IsPaused = false;
        }
    }

    //Set the diary to active if we're in the right game state
    void Inventory(GameManager.GameState previousState, GameManager.GameState currentState){
        if(currentState == GameManager.GameState.INVENTORY){
            diaryPage.SetActive(true);
            Time.timeScale = 0;
        }
        else{
            diaryPage.SetActive(false);
            if(currentState != GameManager.GameState.PAUSED)
                Time.timeScale = 1;
        }
    }

    //Show what items we have (key/diary pages) equipped if we're in a puzzle
    void EquippedItems(GameManager.GameState previousState, GameManager.GameState currentState){
        if(currentState == GameManager.GameState.PUZZLE){
               equippedItems.SetActive(true);
        }
        else{
            equippedItems.SetActive(false);
        }
    }
    void RestartToggle(GameManager.GameState previousState, GameManager.GameState currentState){
        if(currentState == GameManager.GameState.TOWN || currentState == GameManager.GameState.DIALOGUE){
            inPuzzle = false;
        }
    }

    //Show if we have a key or diary page equipped
    public void SetEquippedSprite(ItemInformation item){
        if(item.keyItem){
            item1.GetComponent<Image>().sprite = item.GetSprite();
            item1.GetComponent<Image>().color = Color.white;
        }
        else if(item.equipItem){
            item2.GetComponent<Image>().sprite = item.GetSprite();
            item2.GetComponent<Image>().color = Color.white;
            if(item.itemType == ItemInformation.ItemType.PuzzleCreatePage){
                item2.GetComponentInChildren<Text>().color = Color.black;
            }
            else{
                item2.GetComponentInChildren<Text>().color = Color.white;
            }
        }
    }

    //Set the number on the page to how many more uses we have left
    public void SetNumPages(string pgs){
        item2.GetComponentInChildren<Text>().text = pgs;
    }

    //Remove the key from the equip slot
    public void RemoveKeyItemSprite(){
        item1.GetComponent<Image>().sprite = null;
        item1.GetComponent<Image>().color = new Color(1,1,1,0);
    }
    
    //Remove the diary page from the equip slot
    public void RemoveEquippedSprite(){
        item2.GetComponent<Image>().sprite = null;
        item2.GetComponent<Image>().color = new Color(1,1,1,0);
        SetNumPages("");
    }

    private void OnEnable() {
        controls.UI.Enable();
        controls.City.Enable();
    }

    private void OnDisable() {
        controls.UI.Disable();
        controls.City.Disable();
    }

}
