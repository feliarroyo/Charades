using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public string parameter = "default";
    public Toggle toggle = null;
    // Start is called before the first frame update
    void Start()
    {
        bool value = false;
        if (PlayerPrefs.GetInt(parameter, 1)==1){
            value = true;
        }
        toggle.isOn = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(){
        if (parameter=="showScreenButtons")
            Config.SetToggle(toggle.isOn);
        else
            Config.SetMotionToggle(toggle.isOn);
        if (PlayerPrefs.GetInt("showScreenButtons",1)!=1 && PlayerPrefs.GetInt("useMotionControls", 1)!=1)
            if (parameter=="useMotionControls")
                GameObject.Find("Button Toggle").GetComponent<Toggle>().isOn = true;
            else
                GameObject.Find("Motion Toggle").GetComponent<Toggle>().isOn = true;
    }
}