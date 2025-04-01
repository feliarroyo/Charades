using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages music to be played 
/// </summary>
public class MusicPlayer : MonoBehaviour
{
    public AudioClip clip;

    void Start()
    {
        if (!GameObject.Find("Music").GetComponent<AudioSource>().clip.Equals(clip))
        {
            PlayTrack();
            GameObject.Find("Music").GetComponent<AudioSource>().Play();
        }
    }
    
    /// <summary>
    /// Sets up the player's track in the Music's Audio Source.
    /// </summary>
    public void PlayTrack(){
        GameObject.Find("Music").GetComponent<AudioSource>().clip = this.clip;
    }

    /// <summary>
    /// Stops the music track currently playing.
    /// </summary>
    public static void StopMusic(){
        GameObject.Find("Music").GetComponent<AudioSource>().Stop();
    }
}