using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowItem : MonoBehaviour
{
    public GameObject go_toshow;
    
    public void EnableItem(){
        go_toshow.SetActive(true);
    }

    public void HideItem(){
        go_toshow.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
