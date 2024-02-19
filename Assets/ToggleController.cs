using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public string parameter = "default";
    public Toggle toggle = null;
    public TextMeshProUGUI text = null; // TMPro that shows value, if needed
    // Start is called before the first frame update
    void Start()
    {
        toggle.isOn = Config.showScreenButtons;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(){
        Config.SetToggle(toggle.isOn);
    }
}