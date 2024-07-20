using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasActivator : MonoBehaviour
{
    public bool activeOnCustom;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(activeOnCustom == Config.customMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
