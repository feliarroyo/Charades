using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    public bool setsCustom = false;
    public void SetCurrentMode(){
        Config.customMenu = setsCustom;
    }

    public void SetCurrentEditMode(){
        Config.creatingNewCategory = setsCustom;
    }
}
