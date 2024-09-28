using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    public AudioClip pauseSound;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }

    public void PauseGame(){
        // Pauses if not paused and vice versa
        SFXManager.instance.PlayClip(pauseSound);
        if (isPaused){
            Time.timeScale = 1;
            gameObject.SetActive(false);
            }
        else{
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }
        isPaused = !isPaused;
    }

    public void UnpauseToTransition(string nextScene){
        // same as PauseGame, but only affects time. For use in transitions only
        if (isPaused)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
        isPaused = !isPaused;
        GetComponent<SceneLoader>().LoadScene(nextScene);
    }
}
