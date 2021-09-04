using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : Singleton<GameManager>
{
    public enum GameState{
        MENU,
        TOWN,
        PUZZLE,
        PAUSED,
        GAMEOVER,
        DIALOGUE,
        INVENTORY
    }

    #region Public Vars
    public delegate void GameStateChanged(GameState current, GameState changeTo);
    //Event that occurs when the game state changes
    public static event GameStateChanged onGameStateChanged;
    //System managers that should be spawned in by the game manager
    public GameObject[] systemManagers;
    public bool fadingIn;
    public bool fadingOut;
    public int day = 2;
    public GameState CurrentGameState{
        get{ return currentGameState; }
    }
    #endregion
    
    #region Private Vars
    //List of the currently spawned in game managers
    List<GameObject> instantiatedSystemManagers;
    string currentSceneName;
    GameState currentGameState = GameState.MENU;
    Save saveFile;
    Vector3 spawnPos = Vector3.zero;
    Vector3 LoadCameraPos = Vector3.zero;
    bool loading;
    #endregion

    private void Start() {
        DontDestroyOnLoad(gameObject);
        instantiatedSystemManagers = new List<GameObject>();
        InstantiateSystemManagers();
        LoadScene("Main Menu");
    }

    //Loads a scene in a vertical/horizontal orientation with Ky at a certain position, possible have a fade effect
    public void LoadScene(string sceneName, bool fadeIn = false, bool fadeOut = false, bool loadHor = false, float x = 0, float y = 0){
        GameObject camera = GameObject.Find("Main Camera");
        GameObject ky = GameObject.Find("Ky");
        //Position of the camera to be loaded in
        if(currentGameState == GameState.TOWN && ky){
            LoadCameraPos = camera.transform.position;
            LoadCameraPos = ky.transform.position - LoadCameraPos;
        }
        if(fadeOut){
            fadingOut = true;
        }
        if(fadeIn){
            fadingIn = true;
            StartCoroutine(FadeScene(sceneName));
        }
        else{
            //Unload the current scene
            if(SceneManager.GetActiveScene().name != "StartUpScene"){
                UnloadScene(SceneManager.GetActiveScene().name);
            }
            //Load in the new scene in the background
            AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            if(ao == null)
            {
                Debug.LogError("Unable to Load Level: " + sceneName);
                return;
            }
            currentSceneName = sceneName;
            ao.completed += OnLoadComplete;
        }
        //Set the spawn position of Ky and the camera
        spawnPos = new Vector3(x,y,0);
        if(loadHor){
            LoadCameraPos = new Vector3(spawnPos.x + LoadCameraPos.x,spawnPos.y - LoadCameraPos.y, -10);
        }
        else{
            LoadCameraPos = new Vector3(spawnPos.x - LoadCameraPos.x,spawnPos.y - LoadCameraPos.y, -10);
        }
    }

    //Have the scene fade in as its loading out
    IEnumerator FadeScene(string sceneName){
        UIManager.Instance.FadeIn();
        float aTime = 1.0f;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            yield return null;
        }
        fadingIn = false;
        UnloadScene(SceneManager.GetActiveScene().name);
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if(ao == null)
        {
            Debug.LogError("Unable to Load Level: " + sceneName);
            yield return null;
        }
        currentSceneName = sceneName;
        ao.completed += OnLoadComplete;
        yield return null;
    }

    public void UnloadScene(string sceneName){
        AsyncOperation ao = SceneManager.UnloadSceneAsync(sceneName);
    }

    void OnLoadComplete(AsyncOperation ao){
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
        if(spawnPos != Vector3.zero){
            //Move Ky to where he should be spawned and also move the camera if we're in Ky's room
            GameObject.Find("Ky").transform.position = spawnPos;
            if(SceneManager.GetActiveScene().name != "KyRoom")
                GameObject.Find("Main Camera").transform.position = LoadCameraPos;
        }
        //Move Ky and the camera according to the positions in the save file
        if(loading && saveFile != null){
            Vector3 loadPosition = new Vector3(saveFile.kyCoords[0], saveFile.kyCoords[1], saveFile.kyCoords[2]);
            Vector3 cameraPos = new Vector3(saveFile.cameraCoords[0], saveFile.cameraCoords[1], saveFile.cameraCoords[2]);
            GameObject.Find("Ky").transform.position = loadPosition;
            GameObject.Find("Main Camera").transform.position = cameraPos;
            loading = false;
            UpdateState(saveFile.state);
        }
        if(fadingOut){
            StartCoroutine("FadeOut");
        }
    }

    //have the scene fade out as it's loading in
    IEnumerator FadeOut(){
        UIManager.Instance.FadeOut();
        float aTime = 1.0f;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            yield return null;
        }
        fadingOut = false;
        yield return null;
    }

    /*
    Updates the gamestate and acts according to the new state
    */
    void UpdateState(GameState state){
        GameState previous = currentGameState;
        currentGameState = state;
        //Play different BGMs/sounds depending on the gamestate and location
        switch(currentGameState){
            case GameState.MENU:
                AudioManager.Instance.PlayBGM(AudioManager.Instance.campusBGM);
                break;
            case GameState.TOWN:
                if(SceneManager.GetActiveScene().name == "DiningHall")
                    AudioManager.Instance.PlayBGM(AudioManager.Instance.diningHallBGM);
                else
                    AudioManager.Instance.PlayBGM(AudioManager.Instance.campusBGM);
                break;
            case GameState.PUZZLE:
                AudioManager.Instance.PlayBGM(AudioManager.Instance.puzzleBGM);
                break;
            case GameState.PAUSED:
                break;
            case GameState.GAMEOVER:
                AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.gameOver);
                break;
            case GameState.DIALOGUE:
                break;
            case GameState.INVENTORY:
                break;
            default:
                break;
        }
        if(onGameStateChanged != null){
            onGameStateChanged(previous, currentGameState);
        }
    }

    /*
    Instantiates all the system managers into the scene
    */
    void InstantiateSystemManagers(){
        foreach(GameObject manager in systemManagers){
            instantiatedSystemManagers.Add(Instantiate(manager));
        }
    }

    //Functions to update the state of the game

    public void GameOver(){
        UpdateState(GameState.GAMEOVER);
    }

    public void EnterPuzzle(){
        UpdateState(GameState.PUZZLE);
    }

    public void StartGame(){
        UpdateState(GameState.TOWN);
    }

    public void GoToMenu(){
        UpdateState(GameState.MENU);
    }

    public void Pause(){
        UpdateState(GameState.PAUSED);
    }

    public void EnterInventory(){
        UpdateState(GameState.INVENTORY);
    }

    public void EnterDialogue(){
        UpdateState(GameState.DIALOGUE);
    }

    //Create a save file based on where the player is currently at in the game
    private void CreateSave(){
        GameObject ky = GameObject.Find("Ky");
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        float[] coords = {ky.transform.position.x, ky.transform.position.y, ky.transform.position.z};
        float[] cameraCoords = {camera.transform.position.x, camera.transform.position.y,camera.transform.position.z};
        List<ItemInformation> itemInf = new List<ItemInformation>();
        foreach(ItemInformation item in PlayerInventory.GetInventory()){
            itemInf.Add(item);
        }
        int dir = KyMovementCity.directionFacing;
        saveFile = new Save(coords, cameraCoords, SceneManager.GetActiveScene().name, UIManager.Instance.beforePause, 
                            DecisionManager.Instance.GetDecisionList(), itemInf, Diary.diaryPages, day, dir);
    }

    //Create a save file and save it somewhere
    public void SaveGame(){
        CreateSave();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, saveFile);
        file.Close();
        Debug.Log("Game Saved");
    }

    //Find the savefile and load in the game
    public void LoadGame(){
        //If we have a save file
        if(File.Exists(Application.persistentDataPath + "/gamesave.save")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            saveFile = (Save)bf.Deserialize(file);
            file.Close();
            UnloadScene(SceneManager.GetActiveScene().name);
            loading = true;
            //Load in the scene and all the information that the save file has
            LoadScene(saveFile.scene, false, true);
            DialogueManager.Instance.puzzleDialogueDone = false;
            day = saveFile.day;
            KyMovementCity.directionFacing = saveFile.directionFacing;
            DecisionManager.Instance.SetDecisions(saveFile.decisions);
            PlayerInventory.ClearInventory();
            foreach(ItemInformation item in saveFile.items){
                PlayerInventory.AddItem(item);
            }
            Diary.diaryPages = saveFile.diaryPages;
            Debug.Log("Game Loaded");
        }
        else{
            Debug.LogError("Save file not found");
        }
    }

    /*
    Destroys the system managers along with the game manager
    */
    protected override void OnDestroy()
    {
        foreach(GameObject manager in instantiatedSystemManagers){
            Destroy(manager);
        }
        instantiatedSystemManagers.Clear();
        base.OnDestroy();
    }
}
