using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Class that controls what dialogue is started depending on what choice the player makes
public class ChoiceButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool yes;

    public void ChoiceChosen(){
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.buttonPress);
        DialogueManager dim = DialogueManager.Instance;
        DecisionManager dem = DecisionManager.Instance;
        string choice = dim.GetCurrentDialogue();
        //Depending on the current conversation and if the player chose yes or no, start up the corresponing dialogue
        //Set whether Ky was with Nikko or Simon on certain days for certain dialogues
        switch(choice){
            case "Day1ShedDialogue":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("Day1ShedYes");
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("Day1ShedNo");
                }
                break;
            case "Day1ShedDialogueMultiple":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("Day1ShedYes");
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("Day1ShedNo");
                }
                break;
            case "KyExitDormDay2Dialogue":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("SimonTalkYesDay2");
                    dem.withSimonDay2 = true;
                }
                else{
                    dim.EndConversation();
                    FollowKy.notFollow = true;
                    dim.StartConversation("SimonTalkNoDay2");
                }
                break;
            case "DiningHallDay2Simon":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("DiningHallDay2SimonYes");
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("DiningHallDay2SimonNo");
                }
                break;
            case "LectureDialogueSimonDay2":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("LectureDialogueSimonDay2Yes");
                    dem.wentToClassDay2 = true;
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("LectureDialogueSimonDay2No");
                }
                break;
            case "GymDoorsAfterPassAlone":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("GymDoorsAloneYes");
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("GymDoorsAloneNo");
                }
                break;
            case "GymDoorsAfterPassAloneMultiple":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("GymDoorsAloneYes");
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("GymDoorsAloneNo");
                }
                break;
            case "GymDoorsSimonDialogue": 
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("GymDoorsSimonDialogueYes");
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("GymDoorsSimonDialogueNo");
                }
                break;
            case "NikkoBeforeLectureDialogue":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("TalkNikkoChooseYes");
                    dem.withNikkoDay3 = true;
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("TalkNikkoChooseNo");
                }
                break;
            case "DiningHallKitchenDialogue":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("DiningKitchenChooseYes");
                }
                else{
                    dim.EndConversation();
                }
                break;
            case "DiningHallKitchenDialogue2":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("DiningKitchenChooseYes");
                }
                else{
                    dim.EndConversation();
                }
                break;
            case "DiningHallKitchenAlone":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("DiningKitchenAloneChooseYes");
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("DiningKitchenAloneChooseNo");
                }
                break;
            case "DiningHallKitchenAlone2":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("DiningKitchenAloneChooseYes");
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("DiningKitchenAloneChooseNo");
                }
                break;
            case "DiningHallMealNikkoDialogue":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("MealNikkoChooseYes");
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("MealNikkoChooseNo");
                }
                break;
            case "LibraryDay4DiaryRoute":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("LibraryDay4DiaryRouteYes");
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("LibraryDay4DiaryRouteNo");
                }  
                break;
            case "LibraryDay4Ky":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("LibraryDay4KyYes");
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("LibraryDay4KyNo");
                }  
                break;
            case "LibraryDay4NeutralNikko":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("LibraryDay4NeutralNikkoYes");
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("LibraryDay4NeutralNikkoNo");
                }  
                break;
            case "LibraryDay4NeutralSimon":
                if(yes){
                    dim.EndConversation();
                    dim.StartConversation("LibraryDay4NeutralSimonYes");
                }
                else{
                    dim.EndConversation();
                    dim.StartConversation("LibraryDay4NeutralSimonNo");
                }  
                break;
        }
    }
}
