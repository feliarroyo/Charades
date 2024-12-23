using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPlatformScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CanvasScaler scale = gameObject.GetComponent<CanvasScaler>();
        switch (Application.platform){
            case (RuntimePlatform.Android):
                scale.matchWidthOrHeight = 0;
                break;
            default:
                scale.matchWidthOrHeight = 1;
                break;
        }
    }
}
