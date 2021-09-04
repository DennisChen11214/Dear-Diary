using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Save file that contains all the information needed to load a game
[System.Serializable]
public class Save
{
    //Ky's position in the scene
    public float[] kyCoords;
    //The camera's position in the scene
    public float[] cameraCoords;
    //The name of the scene
    public string scene;
    public GameManager.GameState state;
    //The list of decisions the player has made
    public List<bool> decisions;
    //The list of items the player has
    public List<ItemInformation> items;
    //The list of diary pages the player has
    public List<ItemInformation> diaryPages;
    public int day;
    //What direction Ky was facing
    public int directionFacing;

    public Save(float[] playerPos, float[] cameraPos, string sceneName, GameManager.GameState currentState, List<bool> decisionList, 
                List<ItemInformation> inv, List<ItemInformation> pages, int d, int dir){
        kyCoords = playerPos;
        cameraCoords = cameraPos;
        scene = sceneName;
        state = currentState;
        decisions = decisionList;
        items = inv;
        diaryPages = pages;
        day = d;
        directionFacing = dir;
    }

}
