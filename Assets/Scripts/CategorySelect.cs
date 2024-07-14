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

    // Start is called before the first frame update
    void Start()
    {
        Config.MenuConfig();
        
        // teams get last used named, or default values if first time
        Competition.SetTeamName(0, PlayerPrefs.GetString("Team1", "Equipo 1"));
        Competition.SetTeamName(1, PlayerPrefs.GetString("Team2", "Equipo 2"));

        // creates icons for custom categories saved
        string savePath = Application.persistentDataPath + "/customCategories";
        CategoryCreator.CreateCustomDirectory(savePath);
        foreach (string file in Directory.GetFiles(savePath,"*.json")){
            StreamReader reader = new StreamReader(file);
            GameObject newGameObject = Instantiate(categoryButtonPrefab, customParent.transform);
            newGameObject.GetComponentInChildren<CategoryButton>().jsonCategory = new TextAsset(reader.ReadToEnd());
        }
    }
}