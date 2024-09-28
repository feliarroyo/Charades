using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public bool setsCustom = false;

    public void SetCurrentMode(){
        Config.customMenu = setsCustom;
    }

    public void SetCurrentEditMode(){
        Config.creatingCategory = setsCustom;
    }
}
