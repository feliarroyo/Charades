using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class CategoryEditor : CategoryCreator
{
    //public TextMeshProUGUI category_text, desc_text, question_text, warning_text, questionlist_text;
    //public TMP_Dropdown iconDropdown;
    //string category, description;
    //List<string> questions;
    public static Category originalCategory;
    public string origFileName;
    
    // Start is called before the first frame update
    void Start()
    {
        //if (questions == null)
            InitializeValues();
    }

    public new void InitializeValues(){
        category = originalCategory.category;
        description = originalCategory.description;
        questions = new List<string>();
        Object[] iconArray = Resources.LoadAll("", typeof(Sprite));
        List<TMP_Dropdown.OptionData> iconList = new();
        string iconToSet = null;
        foreach (Object icon in iconArray) {
            TMP_Dropdown.OptionData od = new TMP_Dropdown.OptionData(icon.name, (Sprite) icon);
            iconList.Add(od);
            if (icon.name == originalCategory.iconName){
                iconToSet = icon.name;
            }
        }
        category_text.transform.parent.transform.parent.GetComponent<TMP_InputField>().text = originalCategory.category;
        desc_text.transform.parent.transform.parent.GetComponent<TMP_InputField>().text = originalCategory.description;
        foreach (string s in originalCategory.questions) {
            AddToQuestionList(s);
        }
        origFileName = originalCategory.category.Replace(' ', '_');
        // TBA: put option in opened file as selected option
        iconDropdown.options = iconList;
        if (iconToSet != null)
            SetImage(iconToSet);
    }

    // Creates json file of the new category.
    public new void CreateFile(){
        if (!isCategoryAllowed())
            return;
        string icon = iconDropdown.captionImage.sprite.name;
        Debug.Log("category: " + category + " description: " + description + " icon: " + icon + "\nquestions" + questions);
        Category newCustomCategory = new Category() {
            category=this.category,
            description=this.description,
            iconName=icon,
            questions=this.questions
        };
        string categoryString = JsonUtility.ToJson(newCustomCategory);
        string savePath = Application.persistentDataPath + "/customCategories";
        CreateCustomDirectory(savePath);
        string origPath = savePath + "/" + origFileName + ".json";
        savePath += "/" + category.Replace(' ', '_') + ".json";
        Debug.Log(savePath);
        using StreamWriter streamWriter = new StreamWriter(origPath);
        streamWriter.Write(categoryString);
        streamWriter.Close();
        File.Move(origPath, savePath);
        sceneLoader.LoadLastScene();
    }

    public void DeleteFile() {
        string filePath = Application.persistentDataPath + "/customCategories/" + origFileName + ".json";
        File.Delete(filePath);
        sceneLoader.LoadLastScene();
    }

    IEnumerator ShowWarningText(string warning){
        warning_text.enabled = true;
        warning_text.text = warning;
        yield return new WaitForSeconds(2f);
        warning_text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
