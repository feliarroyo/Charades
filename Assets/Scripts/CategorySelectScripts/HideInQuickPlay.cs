using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInQuickPlay : MonoBehaviour
// Used to hide elements unused in QuickPlay mode
{
    void Start()
    {
        gameObject.SetActive(Competition.game_mode != GameConstants.GameModes.QuickPlay);
    }

}