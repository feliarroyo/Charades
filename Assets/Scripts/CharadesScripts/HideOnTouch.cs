using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(PlayerPrefs.GetInt(GameConstants.PREF_SHOWSCREENBUTTONS, 1) == 1);
    }
}
