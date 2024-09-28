using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CategorySelect : MonoBehaviour
{
    // custom categories attributes
    public GameObject play_mode_prefab, creator_mode_prefab;
    public GameObject play_mode_custom_parent, creator_mode_custom_parent;

    // Start is called before the first frame update
    void Start()
    {
        Config.MenuConfig();
        
        // teams get last used names, or default values if playing for the first time
        Competition.SetTeamName(0, PlayerPrefs.GetString(GameConstants.PREF_TEAM1, GameConstants.DEFAULT_TEAM1));
        Competition.SetTeamName(1, PlayerPrefs.GetString(GameConstants.PREF_TEAM2, GameConstants.DEFAULT_TEAM2));
        
        // creates buttons for the custom categories, in different gameObjects depending on the menu
        if (Config.customMenu)
            CreateCustomCategoryButtons(creator_mode_prefab, creator_mode_custom_parent);
        else
            CreateCustomCategoryButtons(play_mode_prefab, play_mode_custom_parent);
    }

    private void CreateCustomCategoryButtons(GameObject button_prefab, GameObject custom_parent){
        string savePath = Application.persistentDataPath + "/customCategories";
        CategoryCreator.CreateCustomDirectory(savePath);
        foreach (string file in Directory.GetFiles(savePath,"*.json")){
            StreamReader reader = new(file);
            GameObject newGameObject = Instantiate(button_prefab, custom_parent.transform);
            newGameObject.GetComponentInChildren<CategoryButton>().json_category = new TextAsset(reader.ReadToEnd());
            if (Config.customMenu)
                newGameObject.GetComponentInChildren<CategoryButton>().SetSingleSelect();
        }
    }
}