using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Writes the version number in the TMProUGUI text.
/// </summary>
public class WriteVersionNumber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = 'v' + Application.version;
    }
}