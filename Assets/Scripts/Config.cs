using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static bool customMenu = true;
    public static bool creatingNewCategory = true;
    
    // Start is called before the first frame update
    void Start()
    {
        MenuConfig();
        GameObject.Find("Music").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        GameObject.Find("SoundEffects").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("soundVolume", 0.5f);
    }

    /// <summary>
    /// Sets settings for menu interaction (portrait display / allow sleep timeout / play music)
    /// </summary>
    public static void MenuConfig(){
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        if (!GameObject.Find("Music").GetComponent<AudioSource>().isPlaying)
            GameObject.Find("Music").GetComponent<AudioSource>().Play();
    }
    
    /// <summary>
    /// Sets settings for gameplay (landscape display / no sleep timeout or music playing).
    /// Screen orientation is kept as is as long as it's in landscape display.
    /// </summary>
    public static void GameplayConfig(){
    // settings for gameplay (landscape / no sleep nor music)
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        GameObject.Find("Music").GetComponent<AudioSource>().Stop();
        if (Screen.orientation == ScreenOrientation.LandscapeRight)
            Screen.orientation = ScreenOrientation.LandscapeRight;
        else
            Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    /// <summary>
    /// Set a value for a player preference parameter.
    /// </summary>
    /// <param name="parameter">Label to identify the player preference setting.</param>
    /// <param name="value">Setting value for the given parameter.</param>
    public static void SetValue(string parameter, float value){
        switch (parameter){
            case "music":
                PlayerPrefs.SetFloat(Const.PREF_MUSICVOLUME, value);
                GameObject.Find("Music").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(Const.PREF_MUSICVOLUME, 0.5f);
                return;
            case "sound":
                PlayerPrefs.SetFloat(Const.PREF_SOUNDVOLUME, value);
                GameObject.Find("SoundEffects").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(Const.PREF_SOUNDVOLUME, 0.5f);
                return;
            case "timer":
                PlayerPrefs.SetInt(Const.PREF_ROUNDDURATION, (int) value);
                return;
            case "waitTimer":
                PlayerPrefs.SetFloat(Const.PREF_WAITDURATION, value);
                return;
            case Const.MASHUP_ROUND_COUNT:
                PlayerPrefs.SetInt(Const.MASHUP_ROUND_COUNT, (int) value);
                return;
        }
    }

    /// <summary>
    /// Enable/Disable touchscreen controls in gameplay.
    /// </summary>
    /// <param name="setActive">Whether to activate touchscreen controls or not.</param>
    public static void SetScreenControls(bool setActive){
        PlayerPrefs.SetInt(Const.PREF_SHOWSCREENBUTTONS, setActive? 1 : 0);
    }

    /// <summary>
    /// Enable/Disable motion controls in gameplay.
    /// </summary>
    /// <param name="setActive">Whether to activate tilting controls or not.</param>
    public static void SetMotionControls(bool setActive){
        PlayerPrefs.SetInt(Const.PREF_USEMOTIONCONTROLS, setActive? 1 : 0);
    }

    /// <param name="parameter">String representing a player preference data.</param>
    /// <returns>A player preference value, given a valid parameter string.</returns>
    public static float GetValue(string parameter){
        switch (parameter){
            case "music":
                return PlayerPrefs.GetFloat(Const.PREF_MUSICVOLUME, 0.5f);
            case "sound":
                return PlayerPrefs.GetFloat(Const.PREF_SOUNDVOLUME, 0.5f);
            case "timer":
                WriteTimer("TimeLabel");
                return PlayerPrefs.GetInt(Const.PREF_ROUNDDURATION, 60);
            case "waitTimer":
                WriteTimer("WaitTimeLabel");
                return PlayerPrefs.GetFloat(Const.PREF_WAITDURATION, 1f);
        }
        return -1f;
    }

    /// <summary>
    /// Writes a text label with the corresponding value.
    /// </summary>
    /// <param name="name">Name of the timer written.</param>
    public static void WriteTimer(string name){
        switch (name) {
            case "TimeLabel":
                GameObject.Find(name).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt(Const.PREF_ROUNDDURATION, 60).ToString();
                break;
            case "WaitTimeLabel":
                GameObject.Find(name).GetComponent<TextMeshProUGUI>().text = (Mathf.Round(PlayerPrefs.GetFloat(Const.PREF_WAITDURATION, 1f) * 10.0f) * 0.1f ).ToString();
                break;
        }
    }
}