using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class CategoryCreator : MonoBehaviour
{
    TextMeshProUGUI category_text, desc_text, question_text, warning_text;
    string category, description;
    List<string> questions;

    // Start is called before the first frame update
    void Start()
    {
        // To clean up values from a previous entry
        if (questions == null)
            InitializeValues();
    }

    void InitializeValues(){
        category = "";
        description = "";
        questions = new List<string>();
    }

    public void AddQuestion(){
        if (question_text.text != ""){ // Questions can't be empty, also add verification later
            questions.Add(question_text.text);
        }
    }

    public void SetName(){
        category = category_text.text;
    }

    public void SetDescription(){
        description = desc_text.text;
    }

    public bool isCategoryAllowed(){
        // check custom category names
        if (category == ""){
            warning_text.text = "Debes ponerle un nombre a la categoría.";
            return false;
        }
        if (questions.Count == 0){
            warning_text.text = "La categoría no tiene suficientes enunciados.";
            return false;
        }
        if (description == ""){
            warning_text.text = "La categoría no tiene una descripción.";
            return false;
        }
        return true;
    }

    // Creates json file of the new category.
    public void CreateFile(){
        Category newCustomCategory = new Category() {
            category=this.category,
            description=this.description,
            questions=this.questions
        };
        string categoryString = JsonUtility.ToJson(newCustomCategory);
        string savePath = Application.persistentDataPath + category + ".json";
        using StreamWriter streamWriter = new StreamWriter(savePath);
        streamWriter.Write(categoryString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
