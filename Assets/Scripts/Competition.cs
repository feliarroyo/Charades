using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Competition
{
    public static int gameType = 0;

    // keeps track of the prompts of each category used in a session
    // in order to avoid repeating them when playing a category repeatedly
    public static Dictionary<string, List<string> > sessionCategories = new();
    public static Category categoryQP = new();
    public static List<Category> categories = new();
    public static List<string> teamNames = new() {"Equipo 1", "Equipo 2"};
    public static Dictionary<int, List<int> > scores = new();
    public static int currentCategory = 0;
    public static int currentTeam = 0;
    private const int max_teams = 4;
    public static GameObject cleanCatButton = null;

    public static List<Category> lastGameCategories = new();

    public static void AddCategory(Category cat){
        switch (Competition.gameType){
            case 0:
                categoryQP = cat;
                break;
            default:
                if (!categories.Contains(cat))
                    categories.Add(cat);
                else
                    categories.Remove(cat);
                if (cleanCatButton != null)
                    cleanCatButton.SetActive(categories.Count > 0);
                break;
        }
    }

    public static void ClearCategories(){
        categories.Clear();
        cleanCatButton = GameObject.Find("Limpiar");
        cleanCatButton.SetActive(false);
    }

    public static bool ContainsCategory(Category cat){
        return categories.Contains(cat);
    }

    public static Category GetCategory(){
        switch (gameType){
            case 0:
                return categoryQP;
            case 1:
                return categories[currentCategory];
            case 2:
            default:
                if (categories.Count == 1) // with only one category uses standard presentation
                    return categories[0];
                return new Category(){
                    category="Mash-Up",
                    description="¡Pueden tocar enunciados de cualquiera de las categorías seleccionadas!",
                    iconName="pregunta",
                    questions=new()
                };
        };
    }

    // Gets a prompt list from the session records, to avoid repeated prompts
    public static List<string> GetPrompts(Category c){
        Debug.Log("Session categories: " + sessionCategories);
        if (!sessionCategories.ContainsKey(c.category)){
            sessionCategories[c.category] = c.questions;
            Debug.Log("New category for the session! Initializing new session category");
        }
        return sessionCategories[c.category];
        
    }
    
    public static Dictionary<string, List<string> > GetMashUpPrompts(){
        Dictionary<string, List<string> > res = new();
        foreach (Category c in categories)
            res[c.category] = GetPrompts(c);
        return res;
    }

    public static Category GetRandomMashUpCategory(){
        Category nextCategory = categories[UnityEngine.Random.Range(0, categories.Count)];
        return nextCategory;

    }

    public static void StartCompetition(){
        currentCategory = 0;
        currentTeam = 0;
        for (int i = 0; i < PlayerPrefs.GetInt("teams", 1); i++){
            scores[i] = new List<int>();
        }
    }

    public static void RegisterScore(int score){
        scores[currentTeam].Add(score);
        SetNextGame();
    }

    public static bool HasCategories(){
        switch (Competition.gameType){
            case 0:
                return true;
            default:
                return categories.Count != 0;
        }
    }

    public static void SetNextGame(){
        if (currentTeam < PlayerPrefs.GetInt("teams", 1)-1)
            currentTeam++;
        else {
            currentTeam = 0;
            currentCategory++;
            Debug.Log("Equipo " + currentTeam + " juega. Numero de categoria " + currentCategory);
            if ((currentCategory == categories.Count) || (gameType != 1)) {
                SceneManager.LoadScene("FinalResults");
                return;
            }
        }
        MusicPlayer.StopMusic();
        SceneManager.LoadScene("Presentation");
    }

    public static bool AddTeam(int n){
        int cur_teams = PlayerPrefs.GetInt("teams", 1)+n;
        if ((cur_teams > max_teams) || (cur_teams < 1))
            return false;
        PlayerPrefs.SetInt("teams", cur_teams);
        for (int i = 0; i < n; i++){
            teamNames.Add("Equipo " + (i+1));
        }
        return true;
    }

    public static string GetNextTeamName(){
        if (PlayerPrefs.GetInt("teams", 1)==1)
            return "que adivines";
        return teamNames[currentTeam];
    }

    public static void SetTeamName(int i, string name){
        try {
            teamNames[i] = name;
        }
        catch (Exception e){
            Debug.Log(e);
        }
    }

    public static void SetGameType(int id){
        gameType = id;
    }
}