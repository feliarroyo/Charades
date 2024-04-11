using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CategorySelect : MonoBehaviour
{
    public MusicPlayer music;
    public GameObject categoryButtonPrefab; // used for custom category loading
    public GameObject customParent; // used for custom category loading
    // Start is called before the first frame update
    void Start()
    {
        Competition.SetTeamName(1, PlayerPrefs.GetString("Team1", "Equipo 1"));
        Competition.SetTeamName(2, PlayerPrefs.GetString("Team2", "Equipo 2"));
        //categoryButtonPrefab.transform.parent = customParent.transform;
        string savePath = Application.persistentDataPath + "/customCategories";
        CategoryCreator.CreateCustomDirectory(savePath);
        foreach (string file in Directory.GetFiles(savePath,"*.json")){
            StreamReader reader = new StreamReader(file);
            GameObject newGameObject = Instantiate(categoryButtonPrefab, customParent.transform);
            newGameObject.GetComponentInChildren<CategoryButton>().jsonCategory = new TextAsset(reader.ReadToEnd());
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
