using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInQuickPlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Unused in Quick Play
        gameObject.SetActive(Competition.gameType != 0);
    }

}