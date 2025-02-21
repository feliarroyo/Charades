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

    /// <summary>
    /// Stores current categories selected to remember the selection for next time the menu is entered.
    /// </summary>
    public void StoreCategories(){
        Competition.lastGameCategories = Competition.categories;
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

    public void SetGameType(Const.GameModes gameType){
        Competition.SetGameType(gameType);
    }

    public void SetGameType(int gameType){
        Competition.SetGameType((Const.GameModes) gameType);
    }
}
