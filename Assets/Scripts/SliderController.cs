using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public string parameter = "default";
    public Slider slider = null;
    public TextMeshProUGUI text = null; // TMPro that shows value, if needed
    // Start is called before the first frame update
    void Start()
    {
        slider.value = Config.GetValue(parameter);
    }

    public void SetValue(){
        Config.SetValue(parameter, slider.value);
        if (text != null)
            text.text = (Mathf.Round(slider.value *10.0f) * 0.1f ).ToString();
    }

    public void IncreaseValue(){
        slider.value++;
    }

    public void IncreaseValueFloat(){
        slider.value = slider.value+0.1f;
    }

    public void DecreaseValue(){
        slider.value--;
    }

    public void DecreaseValueFloat(){
        slider.value = slider.value-0.1f;
    }
}
