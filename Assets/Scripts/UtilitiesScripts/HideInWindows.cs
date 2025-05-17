using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInWindows : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_STANDALONE_WIN // Any Windows build created should omit this toggle.
            gameObject.SetActive(false);
            return;
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
