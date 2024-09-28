using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetVersionText : MonoBehaviour
{
    // The script writes the version text on the TextMeshProUGUI component.
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = 'v' + Application.version;
    }
}
