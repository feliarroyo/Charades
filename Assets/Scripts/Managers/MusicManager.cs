using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Singleton design pattern
    public static MusicManager instance;
    [SerializeField] private AudioSource MusicObject;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void PlayMusic(AudioClip audioClip) {
        // Music should change only if it's a different track
        if (!MusicObject.clip.Equals(audioClip)) {
            // Assign clip
            MusicObject.clip = audioClip;
            MusicObject.Play();
        }
    }

    public void StopMusic() {
        MusicObject.Stop();
    }

    public bool IsPlaying() {
        return MusicObject.isPlaying;
    }
}