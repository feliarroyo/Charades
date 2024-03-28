using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Competition
{
    public static List<Category> categories = new();
    public static List<string> teamNames = new() {"empty", "Equipo 1", "Equipo 2"};
    public static Dictionary<int, List<int> > scores = new();
    public static int currentCategory = 0;
    public static int currentTeam = 1;
    private const int max_teams = 4;

    public static List<Category> lastGameCategories = new();

    public static int AddCategory(Category cat){
        if (!categories.Contains(cat)){
            categories.Add(cat);
            return 1;
        }
        else
            categories.Remove(cat);
            return 0;
    }

    public static void ClearCategories(){
        categories.Clear();
    }

    public static bool ContainsCategory(Category cat){
        return categories.Contains(cat);
    }

    public static Category GetCategory(){
        return categories[currentCategory];
    }

    public static void StartCompetition(){
        Debug.Log("LLAMA A StartCompetition");
        currentCategory = 0;
        currentTeam = 1;
        for (int i = 1; i <= PlayerPrefs.GetInt("teams", 1); i++){
            scores[i] = new List<int>();
        }
    }

    public static void RegisterScore(int score){
        scores[currentTeam].Add(score);
        SetNextGame();
    }

    public static bool HasCategories(){
        return categories.Count != 0;
    }

    public static void SetNextGame(){
        if (currentTeam < PlayerPrefs.GetInt("teams", 1))
            currentTeam++;
        else {
            currentTeam = 1;
            currentCategory++;
            Debug.Log("Equipo " + currentTeam + " juega. Numero de categoria " + currentCategory);
            if (currentCategory == categories.Count) {
                SceneManager.LoadScene("FinalResults"); // should be results or something
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
        for (int i = 1; i <= n; i++){
            teamNames.Add("Equipo " + n);
        }
        return true;
    }

    public static string GetNextTeamName(){
        if (PlayerPrefs.GetInt("teams", 1)==1)
            {return "que adivines";}
        return teamNames[currentTeam];
    }

    public static void SetTeamName(int i, string name){
        teamNames[i] = name;
    }
}