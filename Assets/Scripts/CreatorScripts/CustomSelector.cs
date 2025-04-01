using UnityEngine;
using TMPro;

/// <summary>
/// Changes button names depending on functionality in custom
/// </summary>
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
