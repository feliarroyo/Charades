using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CompetitionWrapper : MonoBehaviour
// wrapper used to allow OnClick method calls to Competition
{
    public void AddCategory(Category cat){
        Competition.AddCategory(cat);
    }

    public void ClearCategories(){
        Competition.ClearCategories(); 
        foreach (CategoryButton cg in FindObjectsOfType<CategoryButton>()) {
            cg.SetCategory_unselected();
        }
    }

    public void StartCompetition(){
        Competition.StartCompetition();
    }

    public void LoadSceneIfCategories(string sceneName){
        // loads scene only if there is at least one category loaded
        if (Competition.HasCategories()){
            SceneManager.LoadScene(sceneName);
            Competition.lastGameCategories = Competition.categories;
        }
    }

    public void LoadNextScene(string sceneName){
        // loads scene
        SceneManager.LoadScene(sceneName);
    }

    public void SetTeamName(int i){
        TMP_InputField input = GetComponent<TMP_InputField>();
        Competition.SetTeamName(i, input.text);
    }

    public void SetMenuConfig(){
        Config.MenuConfig();
    }

    public void SetGameplayConfigIfCategories(){
        if (Competition.HasCategories()){
            Config.GameplayConfig();
        }
    }
}
