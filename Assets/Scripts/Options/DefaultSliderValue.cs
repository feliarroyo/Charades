using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultSliderValue : MonoBehaviour
{
    public string valueToCall = "default";
    public float defaultValue = 1f;

    
    protected void Start()
    {
        Debug.Log(valueToCall);
        Debug.Log(defaultValue);
        Debug.Log("PP: " + PlayerPrefs.GetFloat(valueToCall, defaultValue));
        GetComponent<Slider>().value = PlayerPrefs.GetFloat(valueToCall, defaultValue);
    }

    public void SaveValue(float value){
        PlayerPrefs.SetFloat(valueToCall, value);
    }

}