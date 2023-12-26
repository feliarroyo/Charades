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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(){
        Config.SetValue(parameter, slider.value);
        if (text != null)
            text.text = slider.value.ToString();
    }
}
