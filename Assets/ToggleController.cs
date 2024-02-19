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
        if (parameter=="button")
            toggle.isOn = Config.showScreenButtons;
        else 
            toggle.isOn = Config.useMotionControls;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(){
        if (parameter=="button")
            Config.SetToggle(toggle.isOn);
        else
            Config.SetMotionToggle(toggle.isOn);
        if (!Config.showScreenButtons && !Config.useMotionControls)
            if (parameter=="motion")
                GameObject.Find("Button Toggle").GetComponent<Toggle>().isOn = true;
            else
                GameObject.Find("Motion Toggle").GetComponent<Toggle>().isOn = true;
    }
}