using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScheme : MonoBehaviour
{
    public bool passType;
    
    // Start is called before the first frame update
    void Start()
    {
        bool showButtons = false;
        if (PlayerPrefs.GetInt("showScreenButtons", 1) == 1)
            showButtons = true;
        gameObject.SetActive(showButtons);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        GameObject.Find("GameLogic").GetComponent<RoundGameplay>().Pass(passType);
    }
}
