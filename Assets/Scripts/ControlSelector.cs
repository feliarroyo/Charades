using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlSelector : MonoBehaviour
{
    private static string[] labels = new string[3]{"Pantalla y sensor", "Pantalla táctil", "Sensor"};
    public TextMeshProUGUI text_space;
   
    // Start is called before the first frame update
    void Start()
    {
        text_space.text = labels[GetCurrentIndex()];
        if ((Application.platform == RuntimePlatform.WindowsPlayer) || (Application.platform == RuntimePlatform.WindowsEditor)){
            gameObject.SetActive(false);
            return;
        }
    }

    private int GetCurrentIndex(){
        if (PlayerPrefs.GetInt(Const.PREF_SHOWSCREENBUTTONS,1) == 1){
            if (PlayerPrefs.GetInt(Const.PREF_USEMOTIONCONTROLS, 1) == 1)
                return 0;
            else
                return 1;
        }
        else
            return 2;
    }

    public void SetNextIndex(){
        switch (GetCurrentIndex()){
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
        text_space.text = labels[GetCurrentIndex()];
    }
}
