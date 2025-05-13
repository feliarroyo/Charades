using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject PauseUI;
    public SceneLoader sceneLoader;

    #if UNITY_STANDALONE_WINDOWS
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }
    #endif

    /// <summary>
    /// Pauses the game if not paused, and vice versa.
    /// </summary>
    public void PauseGame(){
        if (isPaused){
            Time.timeScale = 1;
            PauseUI.SetActive(false);
            }
        else{
            gameObject.GetComponent<SoundEffectPlayer>().PlayClip();
            Time.timeScale = 0;
            PauseUI.SetActive(true);
        }
        isPaused = !isPaused;
    }

    /// <summary>
    /// Same as PauseGame, but only affects time, used for transitions.
    /// </summary>
    /// <param name="nextScene">Scene to transition to.</param>
    public void UnpauseToTransition(string nextScene){
        Time.timeScale = isPaused? 1 : 0;
        isPaused = !isPaused;
        sceneLoader.LoadScene(nextScene);
    }

    /// <summary>
    /// When the pause button clicked, pause the game.
    /// </summary>
    void OnMouseDown(){
        PauseGame();
    }
}
