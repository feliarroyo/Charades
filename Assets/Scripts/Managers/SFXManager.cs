using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // Singleton design pattern
    public static SFXManager instance;
    [SerializeField] private AudioSource SFXObject;
    private void Awake(){
        if (instance == null){
            instance = this;
        }
    }

    public void PlayClip(AudioClip audioClip){
        // Create audio source
        AudioSource audioSource = Instantiate(SFXObject, gameObject.transform);
        // Assign clip
        audioSource.clip = audioClip;
        // Play sound
        audioSource.Play();
        // Destroy after it finishes playing
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

}