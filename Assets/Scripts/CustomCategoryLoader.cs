using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CustomCategoryLoader : MonoBehaviour
{
    public GameObject categoryButtonPrefab;
    public GameObject customParent;
    // Start is called before the first frame update
    void Start()
    {
        string savePath = Application.persistentDataPath + "/customCategories";
        CategoryCreator.CreateCustomDirectory(savePath);
        foreach (string file in Directory.GetFiles(savePath,"*.json")){
            StreamReader reader = new StreamReader(file);
            GameObject newGameObject = Instantiate(categoryButtonPrefab, customParent.transform);
            newGameObject.GetComponentInChildren<CategoryButton>().jsonCategory = new TextAsset(reader.ReadToEnd());
            newGameObject.GetComponentInChildren<CategoryButton>().SetSingleSelect();
        }
    }
}
