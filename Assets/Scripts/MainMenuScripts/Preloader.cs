using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preloader : MonoBehaviour
{
    // Used to load the CategorySelect scene in the background, to reduce loading times
    public SceneLoader preloader;
    void Start()
    {
        preloader.PreloadScene("CategorySelect");
    }

    public void ReadyScene(){
        preloader.LoadPreloaded();
    }
}
