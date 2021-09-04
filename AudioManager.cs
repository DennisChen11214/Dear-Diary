using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    //List of audio sources for the sounds
    public List<AudioSource> audioSources;
    //All the BGM's and sound effects
    public AudioClip campusBGM;
    public AudioClip diningHallBGM;
    public AudioClip puzzleBGM;
    public AudioClip gameOver;
    public AudioClip heartBeat;
    public AudioClip dialogue;
    public AudioClip keyItem;
    public AudioClip sceneTransition;
    public AudioClip menu;
    public AudioClip buttonPress;
    public AudioClip locked;
    public AudioClip moveBlock;
    public AudioClip leaveKy;
    public AudioClip iceMove;
    public AudioClip sleep;

    //The 1st audiosource is always the BGM, play the given BGM and stop the current one
    public void PlayBGM(AudioClip audioClip){
        if(audioClip != audioSources[0].clip){
            audioSources[0].Stop();
            audioSources[0].clip = audioClip;
            audioSources[0].Play();
        }   
    }

    //Find the next available audiosource and play the audio clip
    public void PlaySoundEffect(AudioClip audioClip){
        for(int i = 1 ; i < audioSources.Count; i++){
            if(!audioSources[i].isPlaying){
                audioSources[i].clip = audioClip;
                audioSources[i].Play();
                return;
            }
        }
    }

    //Stop playing all the sound effects
    public void Stop(){
        for(int i = 1 ; i < audioSources.Count; i++){
            if(audioSources[i].isPlaying){
                audioSources[i].Stop();
                return;
            }
        }
    }
}
