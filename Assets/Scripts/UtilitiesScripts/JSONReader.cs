using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    // obtains category data from a given JSON file
    public static Category GetCategory(TextAsset jsonFile){
        return JsonUtility.FromJson<Category>(jsonFile.text);
    }

    public static Category GetCategory(string jsonFile){
        return JsonUtility.FromJson<Category>(jsonFile);
    }
}
