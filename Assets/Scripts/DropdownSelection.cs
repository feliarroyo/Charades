using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownSelection : MonoBehaviour
{

    public static float savedValue;
    // Start is called before the first frame update
    void Awake()
    {

    }

    public void SaveValue() {
        //savedValue = GetComponent<ScrollRect>().verticalScrollbar.value;
        //Debug.Log("savedValue = " + savedValue + " saved");
    }

    public void Upd() {
        Debug.Log("HELP" + gameObject.name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
