using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Button in the ice puzzle that turns certain tiles on/off
public class ButtonTile : MonoBehaviour
{
    //2 sets of tiles that alterate being turned on/off
    [SerializeField] GameObject[] swap1;
    [SerializeField] GameObject[] swap2;
    //Different sprites for the button depending if it's being pressed on
    [SerializeField] Sprite on;
    [SerializeField] Sprite off;
    //Color for the 2 sets of tiles
    Color swap1Color;
    Color swap2Color;

    private void Awake() {
        swap1Color = swap1[0].GetComponent<SpriteRenderer>().color;
        swap2Color = swap2[0].GetComponent<SpriteRenderer>().color;
    }

    private void Start() {
        //Tiles in the second set are initially off
        foreach(GameObject tile in swap2){
            tile.GetComponent<SpriteRenderer>().color = new Color(swap2Color.r,swap2Color.g,swap2Color.b, 0);
            tile.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Button activated when the player steps on it
        if(other.tag == "Player"){
            GetComponent<SpriteRenderer>().sprite = on;
            StopAllCoroutines();
            StartCoroutine("ChangeAlphaOn");
        }
    }

    //Change the first set of tiles off and second set on
    IEnumerator ChangeAlphaOn(){
        Color color = new Color();
        bool allInactive = true;
        //Set the second set of tiles to be active
        foreach(GameObject tile in swap2){
            if(tile != null){
                tile.SetActive(true);
                color = tile.GetComponent<SpriteRenderer>().color;
                allInactive = false;
            }
        }
        if(allInactive)
            yield return null;
        float aTime = 0.25f;
        for (float t = color.a; t < 1.0f; t += Time.deltaTime / aTime)
        {
            //First set of tiles fade out
            foreach(GameObject tile in swap1){
                if(tile != null)
                    tile.GetComponent<SpriteRenderer>().color = new Color(swap1Color.r,swap1Color.g,swap1Color.b, Mathf.Lerp(1,0,t));
            }
            //Second set of tiles fade in
            foreach(GameObject tile in swap2){
                if(tile != null)
                    tile.GetComponent<SpriteRenderer>().color = new Color(swap2Color.r,swap2Color.g,swap2Color.b, Mathf.Lerp(0,1,t));
            }
            yield return null;
        }
        //First set of tiles set inactive
        foreach(GameObject tile in swap1){
            if(tile != null)
                tile.SetActive(false);
        }
    }

    //Change the second set of tiles off and first set on
    IEnumerator ChangeAlphaOff(){
        Color color = new Color();
        bool allInactive = true;
        //Set the first set of tiles to be active
        foreach(GameObject tile in swap1){
            if(tile != null){
                tile.SetActive(true);
                color = tile.GetComponent<SpriteRenderer>().color;
                allInactive = false;
            }
        }
        if(allInactive)
            yield return null;
        float aTime = 0.25f;
        for (float t = color.a; t < 1.0f; t += Time.deltaTime / aTime)
        {
            //First set of tiles fade in
            foreach(GameObject tile in swap1){
                if(tile != null)
                    tile.GetComponent<SpriteRenderer>().color = new Color(swap1Color.r,swap1Color.g,swap1Color.b, Mathf.Lerp(0,1,t));
            }
            //Second set of tiles fade out
            foreach(GameObject tile in swap2){
                if(tile != null)
                    tile.GetComponent<SpriteRenderer>().color = new Color(swap2Color.r,swap2Color.g,swap2Color.b, Mathf.Lerp(1,0,t));
            }
            yield return null;
        }
        //Second set of tiles set inactive
        foreach(GameObject tile in swap2){
            if(tile != null)
                tile.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        //Button activated when the player walks off it
        if(other.tag == "Player"){
            GetComponent<SpriteRenderer>().sprite = off;
            StopAllCoroutines();
            StartCoroutine("ChangeAlphaOff");
        }
    }
}
