using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;

/// <summary>
/// This script contains the behaviour of the control scheme selector 
/// featured in the Options (which is disabled in Windows builds)
/// </summary>
public class ControlSelector : MonoBehaviour
{
    private static string[] labels = new string[6]{"Pantalla o sensor", "Pantalla táctil", "Sensor",
    "Screen or Gyro", "Touchscreen", "Gyroscope"};
    public TextMeshProUGUI text_space;
   
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_STANDALONE_WIN // Any Windows build created should omit this toggle.
            gameObject.SetActive(false);
            return;
        #endif
        text_space.text = GetCurrentLabel();
    }

    /// <summary>
    /// Returns text label corresponding to the current control scheme selected, according to language settings.
    /// </summary>
    /// <returns></returns>
    private string GetCurrentLabel()
    {
        int index = GetCurrentIndex();
        if (Const.EnglishLocaleActive())
            return labels[index + 3];
        else
            return labels[index];
    }

    /// <summary>
    /// 0 = Both schemes; 1 = Only touchscreen; 2 = Only tilt controls
    /// </summary>
    /// <returns>Index corresponding to the current control scheme selected.</returns>
    private int GetCurrentIndex()
    {
        if (PlayerPrefs.GetInt(Const.PREF_SHOWSCREENBUTTONS, 1) == 1)
        {
            if (PlayerPrefs.GetInt(Const.PREF_USEMOTIONCONTROLS, 1) == 1)
                return 0;
            else
                return 1;
        }
        else
            return 2;
    }

    /// <summary>
    /// Sets up the controllers and labels corresponding to the next index, according to the following order:
    /// 0 = Both schemes -> 1 = Only touchscreen -> 2 = Only tilt controls -> 0...
    /// </summary>
    public void SetNextIndex(){
        switch (GetCurrentIndex()){ // Set up next control scheme following the order
            case 0:
                Config.SetMotionControls(false);
                break;
            case 1:
                Config.SetScreenControls(false);
                Config.SetMotionControls(true);
                break;
            default:
                Config.SetScreenControls(true);
                break;
        }
        text_space.text = GetCurrentLabel(); // Label the button properly.
    }
}
