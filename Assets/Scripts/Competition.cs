using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Competition
{
    public static Const.GameModes gameType = Const.GameModes.QuickPlay;

    // keeps track of the prompts of each category used in a session
    // in order to avoid repeating them when playing a category repeatedly
    public static Dictionary<string, List<string>> sessionCategories = new();
    public static Category categoryQP = new();
    public static List<Category> categories = new();
    public static List<string> teamNames = new() { Const.DEFAULT_TEAM1, Const.DEFAULT_TEAM2 };
    public static Dictionary<int, List<int>> scores = new();
    public static int currentCategory = 0;
    public static int currentTeam = 0;
    private const int max_teams = 4;
    public static List<GameObject> multipleCategoryButtons = new();

    public static List<Category> lastGameCategories = new();

    /// <summary>
    /// Adds a category to be played in the current game session. 
    /// If the category has been already added, removes it instead.
    /// </summary>
    /// <param name="cat">Category to be added/removed.</param>
    /// <returns>True if the category has been added, and False if it's been removed.</returns>
    public static bool AddCategory(Category cat)
    {
        switch (gameType)
        {
            case Const.GameModes.QuickPlay:
                categoryQP = cat;
                return true;
            default:
                bool res = !categories.Contains(cat);
                if (!categories.Contains(cat))
                    categories.Add(cat);
                else
                    categories.Remove(cat);
                ShowMultipleCategoryButtons(categories.Count > 0);
                return res;
        }
    }

    /// <summary>
    /// Unselects all categories currently selected.
    /// </summary>
    public static void ClearCategories()
    {
        categories.Clear();
    }

    /// <summary>
    /// Used to show/hide elements that should only be active when there are multiple categories
    /// selected at once. (f.e. Clean Categories and Play Buttons)
    /// </summary>
    /// <param name="show">Whether to show or hide these UI elements.</param>
    public static void ShowMultipleCategoryButtons(bool show)
    {
        foreach (GameObject go in multipleCategoryButtons)
        {
            go.SetActive(show);
        }
    }

    /// <summary>
    /// Checks if a category is in use or not.
    /// </summary>
    /// <param name="cat">Category checked if selected.</param>
    /// <returns>Whether the category parameter is currently selected/in use or not.</returns>
    public static bool ContainsCategory(Category cat)
    {
        return categories.Contains(cat);
    }

    /// <param name="cat">Category which position is checked.</param>
    /// <returns>The position within the category array in which the category is placed.</returns>
    public static int GetCategoryPosition(Category cat)
    {
        return categories.FindIndex(a => a.Equals(cat));
    }

    /// <summary>
    /// Removes category from selection, if currently selected.
    /// </summary>
    /// <param name="cat">Category to remove from selection.</param>
    public static void RemoveCategory(Category cat)
    {
        if (ContainsCategory(cat))
        {
            categories.Remove(cat);
        }
    }

    /// <returns>Category that will be played in the current round.</returns>
    public static Category GetCategory()
    {
        switch (gameType)
        {
            case Const.GameModes.QuickPlay:
                return categoryQP;
            case Const.GameModes.Competition:
                return categories[currentCategory];
            case Const.GameModes.MashUp:
            default:
                if (categories.Count == 1) // one category = standard presentation
                    return categories[0];
                return new Category()
                {
                    title = Const.MASHUP_NAME,
                    description = Const.EnglishLocaleActive()? Const.MASHUP_DESC_EN : Const.MASHUP_DESC,
                    iconName = Const.MASHUP_ICON,
                    questions = new()
                };
        }
        ;
    }

    /// <param name="c">Category from which retrieve session records.</param>
    /// <returns>A prompt list from the session records of the category parameter, to avoid repeated prompts.</returns>
    public static List<string> GetPrompts(Category c)
    {
        if (!sessionCategories.ContainsKey(c.title))
            sessionCategories[c.title] = new(c.questions);
        return sessionCategories[c.title];

    }

    /// <summary>
    /// Sets up competition values to begin a session.
    /// </summary>
    public static void StartCompetition()
    {
        currentCategory = 0;
        currentTeam = 0;

        for (int i = 0; i < PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1); i++)
        {
            scores[i] = new List<int>();
        }
    }

    /// <summary>
    /// Register the score passed as parameter for the current team playing, and moves to the next round.
    /// </summary>
    /// <param name="score"></param>
    public static void RegisterScore(int score)
    {
        scores[currentTeam].Add(score);
        SetNextGame();
    }

    public static bool HasCategories()
    {
        return gameType switch
        {
            Const.GameModes.QuickPlay => true,
            _ => categories.Count != 0,
        };
    }

    /// <summary>
    /// Sets up the next round, with the next team and/or category to be played.
    /// </summary>
    public static void SetNextGame()
    {
        if (currentTeam < PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1) - 1)
            currentTeam++;
        else
        {
            currentTeam = 0;
            currentCategory++;
            if (IsGameOver())
            {
                SceneManager.LoadScene(Const.SCENE_FINALRESULTS);
                return;
            }
        }
        MusicPlayer.StopMusic();
        SceneManager.LoadScene(Const.SCENE_PRESENT);
    }

    /// <summary>
    /// Used to determine if there are any more rounds left to play.
    /// </summary>
    /// <returns>True if the game is over, and False if there are rounds left to play.</returns>
    public static bool IsGameOver()
    {
        return gameType switch
        {
            Const.GameModes.Competition => currentCategory == categories.Count,
            Const.GameModes.MashUp => currentCategory == PlayerPrefs.GetInt(Const.MASHUP_ROUND_COUNT, 1),
            _ => true,
        };
    }

    public static bool AddTeam(int n)
    {
        int cur_teams = PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1) + n;
        if ((cur_teams > max_teams) || (cur_teams < 1))
            return false;
        PlayerPrefs.SetInt(Const.PREF_TEAM_COUNT, cur_teams);
        for (int i = 0; i < n; i++)
        {
            teamNames.Add((Const.EnglishLocaleActive() ? Const.DEFAULT_TEAM_PREFIX_EN : Const.DEFAULT_TEAM_PREFIX) + (i + 1));
        }
        return true;
    }

    public static string GetNextTeamName()
    {
        if (PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1) == 1)
            return Const.NOTEAM;
        return teamNames[currentTeam];
    }

    public static void SetTeamName(int i, string name)
    {
        if (string.IsNullOrWhiteSpace(name)) // Use default names, aren't saved as prefs.
        {
            if (i == 0)
                name = Const.EnglishLocaleActive() ? Const.DEFAULT_TEAM1_EN : Const.DEFAULT_TEAM1;
            else if (i == 1)
                name = Const.EnglishLocaleActive() ? Const.DEFAULT_TEAM2_EN : Const.DEFAULT_TEAM2;
        }
        else // Custom name provided, save it
        {
            PlayerPrefs.SetString("Team" + (i + 1), name);
        }
        teamNames[i] = name;
    }

    public static void SetGameType(Const.GameModes id)
    {
        gameType = id;
    }

    public static int GetCurrentCategoryPosition()
    {
        return categories.Count;
    }

    /// <returns>Prompt pool according to the current game mode.</returns>
    internal static IPromptPool GetPromptPool()
    {
        return gameType switch
        {
            Const.GameModes.MashUp => new MashUpPool(),
            _ => new DefaultPool(),
        };
    }
}