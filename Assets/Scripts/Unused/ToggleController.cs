using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public string parameter = "default";
    public Toggle toggle = null;

    public AudioClip pressSound;
    private bool playSound;
    // Start is called before the first frame update
    void Start()
    {
        if ((Application.platform == RuntimePlatform.WindowsPlayer) || (Application.platform == RuntimePlatform.WindowsEditor)){
            gameObject.SetActive(false);
            return;
        }
        playSound = false;
        bool value = false;
        if (PlayerPrefs.GetInt(parameter, 1)==1){
            value = true;
        }
        toggle.isOn = value;
        playSound = true;
    }

    public void SetValue(){
        if (playSound)
            SFXManager.instance.PlayClip(pressSound);
        if (parameter==GameConstants.PREF_SHOWSCREENBUTTONS)
            Config.SetScreenControls(toggle.isOn);
        else
            Config.SetMotionControls(toggle.isOn);
        if (PlayerPrefs.GetInt(GameConstants.PREF_SHOWSCREENBUTTONS,1)!=1 && PlayerPrefs.GetInt(GameConstants.PREF_USEMOTIONCONTROLS, 1)!=1)
            if (parameter==GameConstants.PREF_USEMOTIONCONTROLS)
                GameObject.Find("Button Toggle").GetComponent<Toggle>().isOn = true;
            else
                GameObject.Find("Motion Toggle").GetComponent<Toggle>().isOn = true;
    }
}