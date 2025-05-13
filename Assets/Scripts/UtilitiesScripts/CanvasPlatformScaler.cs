using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to change how the Canvas is scale depending on the application platform.
/// </summary>
public class CanvasPlatformScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CanvasScaler scale = gameObject.GetComponent<CanvasScaler>();
        scale.matchWidthOrHeight = Application.platform switch
        {
            RuntimePlatform.Android => 0,
            _ => (float)1,
        };
    }
}
