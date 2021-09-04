using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

//Controls the material of the objects in the library scene and the light
public class LibraryAreaScene : MonoBehaviour
{
    [SerializeField] Material nightMaterial;
    [SerializeField] Light2D nightLight;
    void Start()
    {
        //Every night, sets the material of all the objects in the scene to be one that can be lit up and dims the light
        if(GameManager.Instance.day == 1 && DecisionManager.Instance.puzzleDoneDay1){
            SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
            foreach(SpriteRenderer spriteRenderer in allSprites){
                spriteRenderer.material = nightMaterial;
            }
            nightLight.intensity = 0.25f;
        }
        if(GameManager.Instance.day == 2 && DecisionManager.Instance.puzzleDoneDay2){
            SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
            foreach(SpriteRenderer spriteRenderer in allSprites){
                spriteRenderer.material = nightMaterial;
            }
            nightLight.intensity = 0.25f;
        }
        if(GameManager.Instance.day == 3 && DecisionManager.Instance.puzzleDoneDay3){
            SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
            foreach(SpriteRenderer spriteRenderer in allSprites){
                spriteRenderer.material = nightMaterial;
            }
            nightLight.intensity = 0.25f;
        }
    }
}
