using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public AudioClip clip;

    public void PlayClip(){
        GameObject.Find("SoundEffects").GetComponent<AudioSource>().PlayOneShot(clip);
    }
}