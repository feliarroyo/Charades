using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    private const string SFX_VOLUME = "sfxVolume", MUSIC_VOLUME = "musicVolume", MASTER_VOLUME = "masterVolume";
    
    [SerializeField] private AudioMixer audioMixer;

    void Awake(){
        //SetMasterVolume(PlayerPrefs.GetFloat("masterVolume", 1f));
        SetSFXVolume(PlayerPrefs.GetFloat(SFX_VOLUME, 1f));
        SetMusicVolume(PlayerPrefs.GetFloat(MUSIC_VOLUME, 1f));
    }

    public void SetMasterVolume(float level){
        SetMixer(MASTER_VOLUME, level);
    }

    public void SetSFXVolume(float level){
        SetMixer(SFX_VOLUME, level);
    }

    public void SetMusicVolume(float level){
        SetMixer(MUSIC_VOLUME, level);
    }

    private void SetMixer(string mixer, float level){
        float newLevel = Mathf.Log10(level) * 20f;
        Debug.Log("Saved " + mixer + " as " + newLevel + "!!!");
        audioMixer.SetFloat(mixer, newLevel);
    }
}
