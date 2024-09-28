using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public string parameter = "default";
    public TextMeshProUGUI text = null; // TMPro that shows value, if needed

    public void SetValue(float level){
        Config.SetValue(parameter, level);
        if (text != null)
            text.text = (Mathf.Round(level *10.0f) * 0.1f ).ToString();
    }

    public void IncreaseValue(){
        GetComponent<Slider>().value++;
    }

    public void IncreaseValueFloat(){
        GetComponent<Slider>().value += 0.1f;
    }

    public void DecreaseValue(){
        GetComponent<Slider>().value--;
    }

    public void DecreaseValueFloat(){
        GetComponent<Slider>().value -= 0.1f;
    }
}
