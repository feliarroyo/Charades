using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static float musicVolume = 0.5f;
    public static float soundVolume = 0.5f;
    public static int roundDuration = 60;
    public static float answerWaitDuration = 2f;
    public static bool showScreenButtons = false;
    public static bool useMotionControls = true;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Music").GetComponent<AudioSource>().volume = musicVolume;
        GameObject.Find("SoundEffects").GetComponent<AudioSource>().volume = soundVolume;
        if (!GameObject.Find("Music").GetComponent<AudioSource>().isPlaying)
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
        GameObject.Find("Music").GetComponent<AudioSource>().Stop();
        if (Screen.orientation == ScreenOrientation.LandscapeRight)
            Screen.orientation = ScreenOrientation.LandscapeRight;
        else
            Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public static void PreparationConfig(){
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.orientation = ScreenOrientation.AutoRotation;        
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
            case "waitTimer":
                answerWaitDuration = value;
                return;
        }
    }

    public static void SetToggle(bool newValue){
        showScreenButtons=newValue;
    }

    public static void SetMotionToggle(bool newValue){
        useMotionControls=newValue;
    }

    public static float GetValue(string parameter){
        switch (parameter){
            case "music":
                return musicVolume;
            case "sound":
                return soundVolume;
            case "timer":
                Config.WriteTimer("TimeLabel");
                return roundDuration;
            case "waitTimer":
                Config.WriteTimer("WaitTimeLabel");
                return answerWaitDuration;
        }
        return -1f;
    }

    public static void WriteTimer(string name){
        if (name=="TimeLabel")
            GameObject.Find(name).GetComponent<TextMeshProUGUI>().text = roundDuration.ToString();
        else
            GameObject.Find(name).GetComponent<TextMeshProUGUI>().text = (Mathf.Round(answerWaitDuration*10.0f) * 0.1f ).ToString();
    }
}