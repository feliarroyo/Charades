using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static string lastScene = "MainMenu";
    public void LoadScene(string sceneName){
        lastScene = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadSceneAsync(sceneName));
        //SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadSceneAsync(string sceneName){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void LoadLastScene(){
        StartCoroutine(LoadLastSceneAsync());
    }
    IEnumerator LoadLastSceneAsync(){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(lastScene);
        lastScene = SceneManager.GetActiveScene().name;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    public void QuitGame(){
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call<bool>("moveTaskToBack", true);
        // Application.Quit(); // if on PC
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
