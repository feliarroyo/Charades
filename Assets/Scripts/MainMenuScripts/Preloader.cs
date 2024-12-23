using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preloader : MonoBehaviour
{
    // Start is called before the first frame update
    
    public SceneLoader preloader;
    void Start()
    {
        preloader.PreloadScene("CategorySelect");
    }

    public void ReadyScene(){
        preloader.loadPreloaded();
    }
}
