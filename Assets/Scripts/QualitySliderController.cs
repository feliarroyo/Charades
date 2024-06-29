using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QualitySliderController : SliderController
{
    // Start is called before the first frame update
    void Start()
    {
        parameter = "quality";
        slider.value = Config.GetValue(parameter);
    }

    public new void SetValue(){
        Config.SetValue(parameter, slider.value);
        QualitySettings.SetQualityLevel((int) slider.value);
        text.text = ValueNames((int) slider.value);
    }

    public static string ValueNames(int id){
        switch (id){
            case 0:
                return "Bajo";
            case 1:
                return "Medio";
            default:
                return "Alto";
        }
    }
}
