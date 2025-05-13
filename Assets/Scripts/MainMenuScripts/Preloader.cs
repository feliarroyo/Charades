using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Preloads a given scene to reduce load times.
/// </summary>
public class Preloader : MonoBehaviour
{
    // Start is called before the first frame update
    
    public SceneLoader preloader;
    public string nextScene;
    void Start()
    {
        preloader.PreloadScene(nextScene);
    }

    /// <summary>
    /// Goes to the preloaded scene.
    /// </summary>
    public void ReadyScene(){
        preloader.loadPreloaded();
    }
}
