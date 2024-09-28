using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPlatformScaler : MonoBehaviour
{
    // On Android, matching Width is preferable, in other platforms, Height is preferable

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
