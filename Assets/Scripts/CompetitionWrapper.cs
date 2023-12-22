using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CompetitionWrapper : MonoBehaviour
// wrapper used to allow OnClick method calls to Competition
{
    public void AddCategory(Category cat){
        Competition.AddCategory(cat);
    }

    public void StartCompetition(){
        Competition.StartCompetition();
    }

    public void LoadSceneIfCategories(string sceneName){
        // loads scene only if there is at least one category loaded
        if (Competition.HasCategories()){
            Config.GameplayConfig();
            SceneManager.LoadScene(sceneName);
        }
    }

    public void LoadNextScene(string sceneName){
        // loads scene
        Config.MenuConfig();
        SceneManager.LoadScene(sceneName);
    }
}
