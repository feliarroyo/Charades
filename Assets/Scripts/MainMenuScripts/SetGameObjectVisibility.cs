using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObjectVisibility : MonoBehaviour
{
    public void EnableItem(GameObject go){
        go.SetActive(true);
    }

    public void HideItem(GameObject go){
        go.SetActive(false);
    }
}