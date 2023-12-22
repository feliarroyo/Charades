using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip clip;

    public void PlayTrack(){
        GameObject.Find("Music").GetComponent<AudioSource>().clip = this.clip;
    }

    public static void StopMusic(){
        GameObject.Find("Music").GetComponent<AudioSource>().Stop();
    }
}