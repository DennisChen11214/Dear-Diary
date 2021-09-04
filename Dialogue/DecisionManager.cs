using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Has a list of all the respective decisions the player has made on each day
public class DecisionManager : Singleton<DecisionManager>
{
    #region Day1
    public bool diaryCheckedDay1;
    public bool outOfBedDay1;
    public bool beforeClassDay1;
    public bool talkedToAyanaDay1;
    public bool talkedToLucieDay1;
    public bool diningHallTalkDay1;
    public bool interactedWithShed;
    public bool puzzleDoneDay1;
    public bool dialogueAfterPuzzle1;
    #endregion
    #region Day2
    public bool diaryCheckedDay2;
    public bool outOfBedDay2;
    public bool beforeClassDay2;
    public bool wentToClassDay2;
    public bool withSimonDay2;
    public bool talkedToAyanaDay2;
    public bool talkedToCoachDay2;
    public bool diningHallTalkDay2;
    public bool interactedWithGym;
    public bool puzzleDoneDay2;
    public bool dialogueAfterPuzzle2;
    #endregion
    #region Day3
    public bool diaryCheckedDay3;
    public bool beforeClassDay3;
    public bool followKyDay3;
    public bool hasIdCard;
    public bool withNikkoDay3;
    public bool talkedToNikkoDay3;
    public bool talkedToLucieDay3;
    public bool interactedWithKitchen;
    public bool diningHallTalkDay3;
    public bool puzzleDoneDay3;
    public bool outOfBedDay3;
    public bool dialogueAfterPuzzle3;
    #endregion
    #region Day4
    public bool diaryCheckedDay4;
    public bool outOfBedDay4;
    public bool beforeClassDay4;
    public bool puzzleDoneDay4;
    public bool dialogueAfterPuzzle4;
    #endregion

    //Get the list of all the decisions the player made, used for saving/loading
    public List<bool> GetDecisionList(){
        List<bool> decisionList = new List<bool>();
        decisionList.Add(diaryCheckedDay1);
        decisionList.Add(outOfBedDay1);
        decisionList.Add(beforeClassDay1);
        decisionList.Add(talkedToAyanaDay1);
        decisionList.Add(talkedToLucieDay1);
        decisionList.Add(diningHallTalkDay1);
        decisionList.Add(interactedWithShed);
        decisionList.Add(puzzleDoneDay1);
        decisionList.Add(dialogueAfterPuzzle1);
        decisionList.Add(diaryCheckedDay2);
        decisionList.Add(outOfBedDay2);
        decisionList.Add(beforeClassDay2);
        decisionList.Add(wentToClassDay2);
        decisionList.Add(withSimonDay2);
        decisionList.Add(talkedToAyanaDay2);
        decisionList.Add(talkedToCoachDay2);
        decisionList.Add(diningHallTalkDay2);
        decisionList.Add(interactedWithGym);
        decisionList.Add(puzzleDoneDay2);
        decisionList.Add(dialogueAfterPuzzle2);
        decisionList.Add(diaryCheckedDay3);
        decisionList.Add(beforeClassDay3);
        decisionList.Add(followKyDay3);
        decisionList.Add(hasIdCard);
        decisionList.Add(withNikkoDay3);
        decisionList.Add(talkedToNikkoDay3);
        decisionList.Add(talkedToLucieDay3);
        decisionList.Add(interactedWithKitchen);
        decisionList.Add(puzzleDoneDay3);
        decisionList.Add(outOfBedDay3);
        decisionList.Add(dialogueAfterPuzzle3);
        decisionList.Add(diaryCheckedDay4);
        decisionList.Add(outOfBedDay4);
        decisionList.Add(beforeClassDay4);
        decisionList.Add(puzzleDoneDay4);
        decisionList.Add(dialogueAfterPuzzle4);
        decisionList.Add(diningHallTalkDay3);
        return decisionList;
    }

    //Set the current decisions based on a given list, used for saving/loading
    public void SetDecisions(List<bool> decisionList){
        diaryCheckedDay1 = decisionList[0];
        outOfBedDay1 = decisionList[1];
        beforeClassDay1 = decisionList[2];
        talkedToAyanaDay1 = decisionList[3];
        talkedToLucieDay1 = decisionList[4];
        diningHallTalkDay1 = decisionList[5];
        interactedWithShed = decisionList[6];
        puzzleDoneDay1 = decisionList[7];
        dialogueAfterPuzzle1 = decisionList[8];
        diaryCheckedDay2 = decisionList[9];
        outOfBedDay2 = decisionList[10];
        beforeClassDay2 = decisionList[11];
        wentToClassDay2 = decisionList[12];
        withSimonDay2 = decisionList[13];
        talkedToAyanaDay2 = decisionList[14];
        talkedToCoachDay2 = decisionList[15];
        diningHallTalkDay2 = decisionList[16];
        interactedWithGym = decisionList[17];
        puzzleDoneDay2 = decisionList[18];
        dialogueAfterPuzzle2 = decisionList[19];
        diaryCheckedDay3 = decisionList[20];
        beforeClassDay3 = decisionList[21];
        followKyDay3 = decisionList[22];
        hasIdCard = decisionList[23];
        withNikkoDay3 = decisionList[24];
        talkedToNikkoDay3 = decisionList[25];
        talkedToLucieDay3 = decisionList[26];
        interactedWithKitchen = decisionList[27];
        puzzleDoneDay3 = decisionList[28];
        outOfBedDay3 = decisionList[29];
        dialogueAfterPuzzle3 = decisionList[30];
        diaryCheckedDay4 = decisionList[31];
        outOfBedDay4 = decisionList[32];
        beforeClassDay4 = decisionList[33];
        puzzleDoneDay4 = decisionList[34];
        dialogueAfterPuzzle4 = decisionList[35];
        diningHallTalkDay3 = decisionList[36];
    }

    //Set all the decisions to be false, used when starting a new game
    public void Clear(){
        diaryCheckedDay1 = false;
        outOfBedDay1 = false;
        beforeClassDay1 = false;
        talkedToAyanaDay1 = false;
        talkedToLucieDay1 = false;
        diningHallTalkDay1 = false;
        interactedWithShed = false;
        puzzleDoneDay1 = false;
        dialogueAfterPuzzle1 = false;
        diaryCheckedDay2 = false;
        outOfBedDay2 = false;
        beforeClassDay2 = false;
        wentToClassDay2 = false;
        withSimonDay2 = false;
        talkedToAyanaDay2 = false;
        talkedToCoachDay2 = false;
        diningHallTalkDay2 = false;
        interactedWithGym = false;
        puzzleDoneDay2 = false;
        dialogueAfterPuzzle2 = false;
        diaryCheckedDay3 = false;
        beforeClassDay3 = false;
        followKyDay3 = false;
        hasIdCard = false;
        withNikkoDay3 = false;
        talkedToNikkoDay3 = false;
        talkedToLucieDay3 = false;
        interactedWithKitchen = false;
        puzzleDoneDay3 = false;
        outOfBedDay3 = false;
        dialogueAfterPuzzle3 = false;
        diaryCheckedDay4 = false;
        outOfBedDay4 = false;
        beforeClassDay4 = false;
        puzzleDoneDay4 = false;
        dialogueAfterPuzzle4 = false;
        diningHallTalkDay3 = false;
    }
}
