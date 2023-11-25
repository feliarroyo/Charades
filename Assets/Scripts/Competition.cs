using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Competition
{
    public static List<Category> categories = new();
    public static int teams = 1;
    public static Dictionary<int, List<int> > scores = new();
    public static int currentCategory = 0;
    public static int currentTeam = 1;

    private const int max_teams = 4;

    public static int AddCategory(Category cat){
        if (!categories.Contains(cat)){
            categories.Add(cat);
            return 1;
        }
        else
            categories.Remove(cat);
            return 0;
    }

    public static bool ContainsCategory(Category cat){
        return categories.Contains(cat);
    }

    public static Category GetCategory(){
        return categories[currentCategory];
    }

    public static void StartCompetition(){
        currentCategory = 0;
        currentTeam = 1;
        for (int i = 1; i <= teams; i++){
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
        if (currentTeam < teams)
            currentTeam++;
        else {
            currentTeam = 1;
            currentCategory++;
            if (currentCategory == categories.Count) {
                SceneManager.LoadScene("Menu"); // should be results or something
                return;
            }
        }
        SceneManager.LoadScene("GameBegin");
    }

    public static bool AddTeam(int n){
        int cur_teams = teams+n;
        if (((cur_teams) > max_teams) || (cur_teams < 1))
            return false;
        teams = cur_teams;
        return true;
    }
}