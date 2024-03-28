using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Config : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Music").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        GameObject.Find("SoundEffects").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("soundVolume", 0.5f);
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
        //Screen.autorotateToLandscapeLeft = true;
        //Screen.autorotateToLandscapeRight = true;
        //Screen.autorotateToPortrait = false;
        //Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        //Screen.orientation = ScreenOrientation.AutoRotation;        
    }

    public static void SetValue(string parameter, float value){
        switch (parameter){
            case "music":
                PlayerPrefs.SetFloat("musicVolume", value);
                GameObject.Find("Music").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
                return;
            case "sound":
                PlayerPrefs.SetFloat("soundVolume", value);
                GameObject.Find("SoundEffects").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("soundVolume", 0.5f);
                return;
            case "timer":
                PlayerPrefs.SetInt("roundDuration", (int) value);
                return;
            case "waitTimer":
                PlayerPrefs.SetFloat("answerWaitDuration", value);
                return;
        }
    }

    public static void SetToggle(bool newValue){
        int newInt = 0;
        if (newValue) newInt++;
        PlayerPrefs.SetInt("showScreenButtons", newInt);
    }

    public static void SetMotionToggle(bool newValue){
        int newInt = 0;
        if (newValue) newInt++;
        PlayerPrefs.SetInt("useMotionControls", newInt);
    }

    public static float GetValue(string parameter){
        switch (parameter){
            case "music":
                return PlayerPrefs.GetFloat("musicVolume", 0.5f);
            case "sound":
                return PlayerPrefs.GetFloat("soundVolume", 0.5f);
            case "timer":
                Config.WriteTimer("TimeLabel");
                return PlayerPrefs.GetInt("roundDuration", 60);
            case "waitTimer":
                Config.WriteTimer("WaitTimeLabel");
                return PlayerPrefs.GetFloat("answerWaitDuration", 2f);
        }
        return -1f;
    }

    public static void WriteTimer(string name){
        if (name=="TimeLabel")
            GameObject.Find(name).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("roundDuration", 60).ToString();
        else
            GameObject.Find(name).GetComponent<TextMeshProUGUI>().text = (Mathf.Round(PlayerPrefs.GetFloat("answerWaitDuration", 2f) * 10.0f) * 0.1f ).ToString();
    }
}