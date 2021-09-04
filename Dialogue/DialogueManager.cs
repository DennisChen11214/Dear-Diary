using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : Singleton<DialogueManager>
{
    public delegate void FollowNikko4();
    //Event for screen to fade in/out in day 3 lecture hall scene after a dialogue is finished
    public static event FollowNikko4 onFollowNikko4;
    public delegate void EnterPuzzle();
    //Event to load in a puzzle after a dialogue is finished
    public static event EnterPuzzle onPuzzleEntered;
    public delegate void LucieFaint();
    //Event for Lucie to faint after a dialogue is finished
    public static event LucieFaint onLucieFainted;
    //If we've already gone through the dialogue in a puzzle
    public bool puzzleDialogueDone;
    PlayerControls controls;
    //List of all the dialogues in the game
    List<Dialogue> dialogues = new List<Dialogue>();
    Dialogue currentDialogue;
    //UI involved in a dialogue scene
    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject dialogueText;
    [SerializeField] GameObject speakerName;
    [SerializeField] GameObject speakerSpriteLeft;
    [SerializeField] GameObject speakerSpriteRight;
    [SerializeField] GameObject dialogueHolder;
    [SerializeField] GameObject choices;
    string speaker;
    int lineNum;
    int speakerNum;
    bool lettersFlowing;
    string curDialogueName;
    bool eating;
    IEnumerator coroutine;
    GameManager.GameState beforeDialogue;
    //List of possible npcs in the game
    List<string> npcs = new List<string>{"Chef", "Coach", "Lucie", "Staff", "Security", "Bro 1", "Bro 2", "Ryan", "Kyle", "???"};

    protected override void Awake()
    {
        base.Awake();
        controls = new PlayerControls();
        controls.Dialogue.Talk.performed += ctx => Proceed();
        //Load in all the dialogues from the xml file
        DialogueContainer dc = DialogueContainer.Load();
        dialogues = dc.dialogues;
        GameManager.onGameStateChanged += DialogueState;
    }

    //Start a dialogue with the given name
    public void StartConversation(string dialogueName){
        //No dialogue if the scene is fading in
        if(GameManager.Instance.fadingIn)
            return;
        //Find out which dialogue the dialogueName corresponds to
        for(int i = 0; i < dialogues.Count;i++){
            if(dialogues[i].dialogueName == dialogueName){
                currentDialogue = dialogues[i];
            }
        }
        beforeDialogue = GameManager.Instance.CurrentGameState;
        eating = false;
        GameManager.Instance.EnterDialogue();
        curDialogueName = dialogueName;
        //Set UI elements active
        dialogueBox.SetActive(true);
        dialogueText.SetActive(true);
        speakerName.SetActive(true);
        speaker = currentDialogue.speakers[0].name;
        if(speaker != "Narrator"){
            speakerSpriteLeft.SetActive(true);
        }
        lineNum = 0;
        speakerNum = 0;
        coroutine = TypeSentence(currentDialogue.speakers[0].text[0].line);
        StartCoroutine(coroutine);
    }

    //Writes out the dialogue text character by character
    IEnumerator TypeSentence(string sentence){
        //Checks if there's a condition for the line to appear
        string condition = currentDialogue.speakers[speakerNum].text[lineNum].condition;
        bool redText = false;
        if(condition != null){
            //Checks if we meet the conditions for the line to appear
            if(condition.Equals("simonWithKyDay2") && !DecisionManager.Instance.withSimonDay2){
                NextLine();
                yield break;
            }
            else if(condition.Equals("simonNotWithKyDay2") && DecisionManager.Instance.withSimonDay2){
                NextLine();
                yield break;
            }
            else if(condition.Equals("kyTalkAyanaSimonDay2") && !DecisionManager.Instance.talkedToAyanaDay2){
                NextLine();
                yield break;
            }
            else if(condition.Equals("kyNoTalkAyanaSimonDay2") && DecisionManager.Instance.talkedToAyanaDay2){
                NextLine();
                yield break;
            }
            //Checks if the text should be made red
            else if(condition.Equals("redText")){
                redText = true;
            }
        }
        //Set the size of the dialogue box and position of the choices
        dialogueText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 600);
        dialogueBox.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 690);
        choices.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
        //Special dialogue boxes for Ky, the narrator, Nikko, Simon, Nikko, Nova, and Ayana
        switch(speaker){
            case "Ky":
                dialogueText.GetComponent<Text>().color = Color.black;
                speakerName.GetComponent<Text>().color = Color.black;
                dialogueBox.GetComponent<Image>().sprite = DialogueAssets.Instance.kyDialogueBox;
                //Get the dialogue sprite corresponding to the line's emotion
                Sprite dSpriteKy = DialogueAssets.Instance.ReturnSprite(currentDialogue.speakers[speakerNum].text[lineNum].emotion);
                speakerSpriteLeft.SetActive(true);
                if(dSpriteKy == null){
                    speakerSpriteLeft.SetActive(false);
                }
                else{
                    speakerSpriteLeft.GetComponent<Image>().sprite = dSpriteKy;
                }
                break;
            case "Narrator":
                speakerName.SetActive(false);
                dialogueText.GetComponent<Text>().color = Color.white;
                //Narrator has no dialogue sprite
                speakerSpriteLeft.SetActive(false);
                dialogueBox.GetComponent<Image>().sprite = DialogueAssets.Instance.narratorDialogueBox;
                //Narrator's dialogue box is smaller
                dialogueText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 450);
                dialogueBox.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500);
                choices.GetComponent<RectTransform>().localPosition = new Vector3(105,0,0);
                break;
            case "Nikko":
                dialogueText.GetComponent<Text>().color = Color.black;
                speakerName.GetComponent<Text>().color = Color.black;
                dialogueBox.GetComponent<Image>().sprite = DialogueAssets.Instance.nikkoDialogueBox;
                Sprite dSpriteNikko = DialogueAssets.Instance.ReturnSprite(currentDialogue.speakers[speakerNum].text[lineNum].emotion);
                speakerSpriteLeft.SetActive(true);
                //Possible for Nikko, Simon, Nova, and Ayana to not have a dialogue sprite in certain conversations
                if(dSpriteNikko == null){
                    speakerSpriteLeft.SetActive(false);
                }
                else{
                    speakerSpriteLeft.GetComponent<Image>().sprite = dSpriteNikko;
                }
                break;
            case "Simon":
                dialogueText.GetComponent<Text>().color = Color.white;
                speakerName.GetComponent<Text>().color = Color.white;
                dialogueBox.GetComponent<Image>().sprite = DialogueAssets.Instance.simonDialogueBox;
                Sprite dSpriteSimon = DialogueAssets.Instance.ReturnSprite(currentDialogue.speakers[speakerNum].text[lineNum].emotion);
                speakerSpriteLeft.SetActive(true);
                if(dSpriteSimon == null){
                    speakerSpriteLeft.SetActive(false);
                }
                else{
                    speakerSpriteLeft.GetComponent<Image>().sprite = dSpriteSimon;
                }
                break;
            case "Nova":
                dialogueText.GetComponent<Text>().color = Color.white;
                speakerName.GetComponent<Text>().color = Color.white;
                dialogueBox.GetComponent<Image>().sprite = DialogueAssets.Instance.novaDialogueBox;
                Sprite dSpriteNova = DialogueAssets.Instance.ReturnSprite(currentDialogue.speakers[speakerNum].text[lineNum].emotion);
                speakerSpriteLeft.SetActive(true);
                if(dSpriteNova == null){
                    speakerSpriteLeft.SetActive(false);
                }
                else{
                    speakerSpriteLeft.GetComponent<Image>().sprite = dSpriteNova;
                }
                break;
            case "Ayana":
                dialogueText.GetComponent<Text>().color = Color.white;
                speakerName.GetComponent<Text>().color = Color.white;
                dialogueBox.GetComponent<Image>().sprite = DialogueAssets.Instance.npcDialogueBox;
                Sprite dSpriteAyana = DialogueAssets.Instance.ReturnSprite(currentDialogue.speakers[speakerNum].text[lineNum].emotion);
                speakerSpriteLeft.SetActive(true);
                if(dSpriteAyana == null){
                    speakerSpriteLeft.SetActive(false);
                }
                else{
                    speakerSpriteLeft.GetComponent<Image>().sprite = dSpriteAyana;
                }
                break;
        }
        //Different dialogue box for NPCs
        if(npcs.Contains(speaker)){
            dialogueText.GetComponent<Text>().color = Color.white;
            if(redText)
                dialogueText.GetComponent<Text>().color = Color.red;
            speakerName.GetComponent<Text>().color = Color.white;
            dialogueBox.GetComponent<Image>().sprite = DialogueAssets.Instance.npcDialogueBox;
            speakerSpriteLeft.SetActive(false);
        }
        lettersFlowing = true;
        //Get the name of the speaker and put it in the dialogue box
        if(speaker != "Narrator"){
            speakerName.GetComponent<Text>().text = speaker;
            speakerName.SetActive(true);
        }
        dialogueText.GetComponent<Text>().text = "";
        //Write out each letter in the line character by character
        foreach(char letter in sentence.ToCharArray()){
            dialogueText.GetComponent<Text>().text += letter;
            yield return new WaitForSeconds(0.03f);
        }
        //Check if choices are available for this dialogue line
        if(currentDialogue.speakers[speakerNum].text[lineNum].choice != null){
            choices.SetActive(true);
        }
        else{
            choices.SetActive(false);
        }
        lettersFlowing = false;
    }

    //Finishes writing out the dialogue line immediately
    private void Proceed(){
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE && !eating){
            if(lettersFlowing){
                //Stops the line from being written out char by char
                StopCoroutine(coroutine);
                lettersFlowing = false;
                //Finishes writing out the line at once
                dialogueText.GetComponent<Text>().text = currentDialogue.speakers[speakerNum].text[lineNum].line;
                //Small delay before the choices appear
                if(currentDialogue.speakers[speakerNum].text[lineNum].choice != null){
                    StartCoroutine("Delay");
                }
                else{
                    choices.SetActive(false);
                }
            }
            else{
                if(currentDialogue.speakers[speakerNum].text[lineNum].choice != null){
                    return;
                }
                NextLine(); 
            }
        }
    }

    //Move to the next line in the dialogue
    private void NextLine(){
        lineNum++;
        //Move to the next speaker if the current one has no mroe lines
        if(lineNum >= currentDialogue.speakers[speakerNum].text.Count){
            lineNum = 0;
            speakerNum++;
        }
        //End the conversation if there are no more speakers
        if(speakerNum >= currentDialogue.speakers.Count){
            EndConversation();
            return;
        }
        //Go to the next line
        speaker = currentDialogue.speakers[speakerNum].name;
        coroutine = TypeSentence(currentDialogue.speakers[speakerNum].text[lineNum].line);
        StartCoroutine(coroutine);
    }

    IEnumerator Delay(){
        yield return new WaitForSeconds(0.05f);
        choices.SetActive(true);
    }

    //End the dialogue by setting all the dialogue UI off and setting the gamestate to what it was before
    public void EndConversation(){
        dialogueBox.SetActive(false);
        dialogueText.SetActive(false);
        speakerName.SetActive(false);
        speakerSpriteLeft.SetActive(false);
        speakerSpriteRight.SetActive(false);
        choices.SetActive(false);
        switch(beforeDialogue){
            case GameManager.GameState.TOWN:
                GameManager.Instance.StartGame();
                break;
            case GameManager.GameState.PUZZLE:
                GameManager.Instance.EnterPuzzle();
                break;
            default:
                GameManager.Instance.StartGame();
                break;
        }
        //Check if there's anything that should happen at the end of this dialogue
        HandleDialogue();
    }

    //Handles all the events that should happen at the end of certain dialogues
    private void HandleDialogue(){
        //Fade in/out event when talking to Nikko on day 3 or Simon on day 2
        if(curDialogueName.Equals("TalkNikkoChooseYes") || curDialogueName.Equals("LectureDialogueSimonDay2Yes")){
            onFollowNikko4();
        }
        //Enter the puzzle at the end of these dialogues
        if(curDialogueName.Equals("DiningKitchenChooseYes") || curDialogueName.Equals("DiningKitchenAloneChooseYes") ||
                curDialogueName.Equals("Day1ShedYes") ||
                curDialogueName.Equals("GymDoorsSimonDialogueYes") || curDialogueName.Equals("GymDoorsAloneYes") ||
                curDialogueName.Equals("LibraryDay4DiaryRouteYes") || curDialogueName.Equals("LibraryDay4KyYes") ||
                curDialogueName.Equals("LibraryDay4NeutralNikkoYes") || curDialogueName.Equals("LibraryDay4NeutralSimonYes")){
            onPuzzleEntered();
        }
        //At the end of the puzzles, go back to campus
        if(curDialogueName.Equals("Day1EndEntry")){
            DecisionManager.Instance.puzzleDoneDay1 = true;
            GameManager.Instance.StartGame();
            puzzleDialogueDone = false;
            KyMovementCity.Face(2);
            GameManager.Instance.LoadScene("LectureArtArea", true, true, false, 6.1f, -2.8f);
        }
        if(curDialogueName.Equals("Day2EndEntrySimon") || curDialogueName.Equals("Day2EndEntryAlone")){
            DecisionManager.Instance.puzzleDoneDay2 = true;
            GameManager.Instance.StartGame();
            puzzleDialogueDone = false;
            KyMovementCity.Face(2);
            GameManager.Instance.LoadScene("DormArea", true, true, false, -14.6f, 8f);
        }
        if(curDialogueName.Equals("Day3EndEntryNikkoSimon") || curDialogueName.Equals("Day3EndEntrySimon") ||
           curDialogueName.Equals("Day3EndEntryNikko") || curDialogueName.Equals("Day3EndEntryAlone")){
            DecisionManager.Instance.puzzleDoneDay3 = true;
            GameManager.Instance.StartGame();
            puzzleDialogueDone = false;
            KyMovementCity.Face(-2);
            GameManager.Instance.LoadScene("DiningHall", true, true, false, 57, 13f);
        }
        if(curDialogueName.Equals("DiningHallLucieDialogue")){
            onLucieFainted();
        }
        //Scene with everyone eating together
        if(curDialogueName.Equals("DiningHallDay1Friends") || curDialogueName.Equals("DiningHallDay2SimonYes") ||
                curDialogueName.Equals("MealNikkoChooseYes")){
            eating = true;
            UIManager.Instance.Eat();
        }
        //Transition from 1st puzzle on day 4 to second
        if(curDialogueName.Equals("Day4Puzzle1NeutralSimonEnd") || curDialogueName.Equals("Day4Puzzle1NeutralNikkoEnd") || 
                curDialogueName.Equals("Day4Puzzle1KyEnd")){
            puzzleDialogueDone = false;
            GameManager.Instance.LoadScene("Day4SecondPuzzle", true, true);
        }
        //Transition from 2nd puzzle on day 4 to 3rd
        if(curDialogueName.Equals("Day4Puzzle2NeutralSimonEnd") || curDialogueName.Equals("Day4Puzzle2KyEnd")){
            puzzleDialogueDone = false;
            GameManager.Instance.LoadScene("Day4ThirdPuzzle", true, true);
        }
        //Transition from 3rd puzzle on day 4 to the rooftop
        if(curDialogueName.Equals("Day4Puzzle3NeutralNikkoEnd") || curDialogueName.Equals("Day4Puzzle3KyEnd")){
            GameManager.Instance.LoadScene("Rooftop", true, true);
            puzzleDialogueDone = false;
        }
        //All the dialogues where a diary page pops on screen afterward
        if(curDialogueName.Equals("diaryDay1Dorm") || curDialogueName.Equals("KyDormDiaryDay2") || 
                curDialogueName.Equals("KyDormDiaryPickUp") || curDialogueName.Equals("KyDiaryDay4DiaryRoute") ||
                curDialogueName.Equals("KyDormDiaryDay4NeutralKy") || curDialogueName.Equals("Day1PuzzleEnd") ||
                curDialogueName.Equals("Day2PuzzleEndSimon") || curDialogueName.Equals("Day2PuzzleEndAlone") ||
                curDialogueName.Equals("PuzzleEndNikkoSimonDialogue") || curDialogueName.Equals("PuzzleEndNikkoDialogue") ||
                curDialogueName.Equals("PuzzleEndSimonDialogue") || curDialogueName.Equals("PuzzleEndAloneDialogue") ||
                curDialogueName.Equals("DiaryRouteEnding")){
            UIManager.Instance.popUp = true;
            UIManager.Instance.ToggleDiaryPage();
        }
        //At the end of each day, Ky sleeps and it becomes the next day
        if(curDialogueName.Equals("ReenterDormAfterPuzzle")){
            UIManager.Instance.Sleep();
            GameManager.Instance.day++;
        }
        if(curDialogueName.Equals("Day1EndDormDialogue")){
            UIManager.Instance.Sleep();
            GameManager.Instance.day++;
        }
        if(curDialogueName.Equals("Day2NightDormAlone") || curDialogueName.Equals("Day2NightDormSimonRoute")){
            UIManager.Instance.Sleep();
            GameManager.Instance.day++;
        }
        //Load in the ending screen
        if(curDialogueName.Equals("NeutralNikkoEnding") || curDialogueName.Equals("NeutralSimonEnd") || 
                curDialogueName.Equals("Day4EndAlone") || curDialogueName.Equals("EpilogueKy")){
            GameManager.Instance.LoadScene("EndingScene", true, true);
        }
        //Special epilogue scene for the good ending
        if(curDialogueName.Equals("KyEnding")){
            KyMovementCity.Face(-2);
            GameManager.Instance.LoadScene("KyRoom", true, true);
            GameManager.Instance.day++;
        }
    }

    private void Skip(){
        if(GameManager.Instance.CurrentGameState == GameManager.GameState.DIALOGUE){
            EndConversation();
        }
    }

    public string GetCurrentDialogue(){
        return curDialogueName;
    }

    void DialogueState(GameManager.GameState previousState, GameManager.GameState currentState){
        if(currentState != GameManager.GameState.DIALOGUE){
            dialogueHolder.SetActive(false);
        }
        else{
            dialogueHolder.SetActive(true);
        }
    }

    private void OnEnable() {
        controls.Dialogue.Enable();
    }

    private void OnDisable() {
        controls.Dialogue.Disable();
    }

}
