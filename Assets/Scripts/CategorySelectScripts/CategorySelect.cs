using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CategorySelect : MonoBehaviour
{
    public MusicPlayer music;

    // custom categories attributes
    public GameObject categoryButtonPrefab;
    public GameObject customParent;
    public static List<CategoryButton> selectedCatButtons = new();

    // Start is called before the first frame update
    void Start()
    {
        Config.MenuConfig();
        
        // teams get last used named, or default values if first time
        Competition.SetTeamName(0, PlayerPrefs.GetString(Const.PREF_TEAM1, Const.DEFAULT_TEAM1));
        Competition.SetTeamName(1, PlayerPrefs.GetString(Const.PREF_TEAM2, Const.DEFAULT_TEAM2));

        // creates buttons for custom categories saved in AppData
        string savePath = Application.persistentDataPath + "/customCategories";
        CategoryCreator.CreateCustomDirectory(savePath);
        foreach (string file in Directory.GetFiles(savePath,"*.json")){
            StreamReader reader = new StreamReader(file);
            GameObject newGameObject = Instantiate(categoryButtonPrefab, customParent.transform);
            newGameObject.GetComponentInChildren<CategoryButton>().jsonCategory = new TextAsset(reader.ReadToEnd());
        }
    }
}