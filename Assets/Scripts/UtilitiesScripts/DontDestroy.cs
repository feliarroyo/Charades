using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to avoid destroying GameObjects between scenes.
/// </summary>
public class DontDestroy : MonoBehaviour
{
    public static HashSet<string> instances = new();
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        string instance = gameObject.name;
        if (!instances.Contains(instance)){
            instances.Add(instance);
        }
        else {
            Destroy(gameObject);
        }
    }

}
