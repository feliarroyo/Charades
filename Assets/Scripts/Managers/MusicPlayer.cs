using System;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip clip;

    void Start(){
        Debug.Log("Playing music... " + clip.name);
        try {
            MusicManager.instance.PlayMusic(clip);
        }
        catch (NullReferenceException) {
            
        }
        
    }

    public static void StopMusic(){
        MusicManager.instance.StopMusic();
    }
}