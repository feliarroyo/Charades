using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Changes button names depending on functionality in custom
public class CustomSelector : MonoBehaviour
{
    public string[] labels;
    public TextMeshProUGUI text_space;
   
    // Start is called before the first frame update
    void Start()
    {
        switch (Config.creatingNewCategory){
            case false:
                text_space.text = labels[1];
                return;
            default:
                text_space.text = labels[0];
                return;
        }
    }
}
