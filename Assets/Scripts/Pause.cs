using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject PauseUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame(){
        if (isPaused){
            Time.timeScale = 1;
            PauseUI.SetActive(false);
            }
        else{
            Time.timeScale = 0;
            PauseUI.SetActive(true);
        }
        isPaused = !isPaused;
    }

    void OnMouseDown(){
        Debug.Log("Click registered");
        PauseGame();
    }
}
