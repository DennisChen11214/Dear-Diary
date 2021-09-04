using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    
    [SerializeField] Toggle toggle;
    [SerializeField] Slider musicVol;
    [SerializeField] AudioSource sounds;

    private void Awake() {
        if(!PlayerPrefs.HasKey("musicEnabled")){
            PlayerPrefs.SetInt("musicEnabled", 1);
            toggle.isOn = true;
            sounds.enabled = true;
            PlayerPrefs.Save();
        }
        else{
            if(PlayerPrefs.GetInt("musicEnabled") == 0){
                sounds.enabled = false;
                toggle.isOn = false;
            }
            else{
                sounds.enabled = true;
                toggle.isOn = true;
            }
        }
    }

    public void ToggleMusic()
    {
        if (toggle.isOn){
            PlayerPrefs.SetInt("sounds", 1);
            sounds.enabled = true;
        }
        else{
            PlayerPrefs.SetInt("sounds", 0);
            sounds.enabled = false;
        }
    }
}
