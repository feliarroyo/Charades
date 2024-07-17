using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    public bool setsCustom = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentMode(){
        Config.customMenu = setsCustom;
    }

    public void SetCurrentEditMode(){
        Config.creatingCategory = setsCustom;
    }
}
