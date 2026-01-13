using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// This class sets up the CategorySelect scene. This includes storing selected CategoryButtons and creating custom buttons.
/// </summary>
public class CategorySelect : MonoBehaviour
{
    // custom categories attributes
    public GameObject categoryButtonPrefab;
    public GameObject customParent;
    public static List<CategoryButton> selectedCatButtons = new();

    // Start is called before the first frame update
    void Start()
    {
        Config.MenuConfig();
        
        // teams get last used named, or default values if first time
        Competition.SetTeamName(0, PlayerPrefs.GetString(Const.PREF_TEAM1, ""));
        Competition.SetTeamName(1, PlayerPrefs.GetString(Const.PREF_TEAM2, ""));
        Competition.multipleCategoryButtons = new List<GameObject>(GameObject.FindGameObjectsWithTag("Button"));
        Competition.ShowMultipleCategoryButtons(
            Competition.gameType != Const.GameModes.QuickPlay && Competition.HasCategories()
        );
        // creates buttons for custom categories saved in AppData
        CustomCategoryLoader.CreateCustomCategoryButtons(categoryButtonPrefab, customParent);
    }
}