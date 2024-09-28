using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefaultSliderValueWithText : DefaultSliderValue
{
    public TextMeshProUGUI text = null; // TMPro that shows value, if needed
    
    protected new void Start()
    {
        base.Start();
        text.text = (Mathf.Round(PlayerPrefs.GetFloat(valueToCall, defaultValue) *10.0f) * 0.1f).ToString();
        
    }

}
