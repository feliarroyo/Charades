using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static float musicVolume = 1;
    public static float soundVolume = 1;
    public static int roundDuration = 60;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Music").GetComponent<AudioSource>().volume = musicVolume;
        GameObject.Find("SoundEffects").GetComponent<AudioSource>().volume = soundVolume;
        MenuConfig();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public static void MenuConfig(){
    // settings for Menu interaction (portrait / allow sleep / music playing)
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        Screen.orientation = ScreenOrientation.Portrait;
        GameObject.Find("Music").GetComponent<AudioSource>().Play();
    }
    
    public static void GameplayConfig(){
    // settings for gameplay (landscape / no sleep nor music)
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        GameObject.Find("Music").GetComponent<AudioSource>().Stop();
    }

    public static void SetValue(string parameter, float value){
        switch (parameter){
            case "music":
                musicVolume = value;
                GameObject.Find("Music").GetComponent<AudioSource>().volume = musicVolume;
                return;
            case "sound":
                soundVolume = value;
                GameObject.Find("SoundEffects").GetComponent<AudioSource>().volume = soundVolume;
                return;
            case "timer":
                roundDuration = (int) value;
                return;
        }
    }
}