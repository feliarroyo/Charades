using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScheme : MonoBehaviour
{
    public bool passType;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(Config.showScreenButtons);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        GameObject.Find("GameLogic").GetComponent<RoundGameplay>().Pass(passType);
    }
}
