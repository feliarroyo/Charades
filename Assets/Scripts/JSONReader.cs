using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public static Category GetCategory(TextAsset jsonFile){
        return JsonUtility.FromJson<Category>(jsonFile.text);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
