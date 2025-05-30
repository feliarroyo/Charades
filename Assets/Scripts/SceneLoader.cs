using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static string lastScene = Const.SCENE_MAINMENU;
    private static bool isLoading = false;
    private bool ready = false;
    public void LoadScene(string sceneName){
        if (isLoading)
            return;
        lastScene = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadSceneAsync(sceneName));
        //SceneManager.LoadScene(sceneName);
    }
    
    void Start(){
        ready = false;
    }

    public void PreloadScene(string sceneName){
        ready = false;
        StartCoroutine(PreloadSceneAsync(sceneName));
    }
    IEnumerator PreloadSceneAsync(string sceneName){
        yield return null;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        isLoading = true;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                isLoading = false;
                if (ready == true)
                    asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void loadPreloaded(){
        ready = true;
    }


    IEnumerator LoadSceneAsync(string sceneName){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        isLoading = true;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                isLoading = false;
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void LoadLastScene(){
        if (isLoading)
            return;
        StartCoroutine(LoadLastSceneAsync());
    }
    IEnumerator LoadLastSceneAsync(){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(lastScene);
        lastScene = SceneManager.GetActiveScene().name;
        isLoading = true;
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                isLoading = false;
                asyncLoad.allowSceneActivation = true;
            }
            
            yield return null;
        }
    }

    public static bool IsLoading(){
        return isLoading;
    }
    public void QuitGame(){
        if (Application.platform == RuntimePlatform.Android){
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
        }
        else
            Application.Quit();
    }
}