using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject PauseUI;
    public SceneLoader sceneLoader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }

    public void PauseGame(){
        // Pauses if not paused and vice versa
        gameObject.GetComponent<SoundEffectPlayer>().PlayClip();
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

    public void UnpauseToTransition(string nextScene){
        // same as PauseGame, but only affects time. For use in transitions only
        if (isPaused)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
        isPaused = !isPaused;
        sceneLoader.LoadScene(nextScene);
    }

    void OnMouseDown(){
        // When clicked, pause the game
        PauseGame();
    }
}
