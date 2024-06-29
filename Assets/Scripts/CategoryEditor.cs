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
    public bool wereChangesMade = false;
    public static bool isInitializing = true;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeValues();
    }

    public void SetChangesWereMade(bool wereChangesMade){
        this.wereChangesMade = wereChangesMade;
    }

    public new void AddQuestion(){
        base.AddQuestion();
        wereChangesMade = true;
    }

    public new void RemoveQuestion(string prompt){
        base.RemoveQuestion(prompt);
        wereChangesMade = true;
    }

    public new void InitializeValues(){
        isInitializing = true;
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
        DeleteablePromptController.canDelete = true;
        foreach (string s in originalCategory.questions) {
            AddToQuestionList(s);
        }
        origFileName = originalCategory.category.Replace(' ', '_');
        // TBA: put option in opened file as selected option
        iconDropdown.options = iconList;
        if (iconToSet != null)
            SetImage(iconToSet);
        wereChangesMade = false;
        isInitializing = false;
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

    public new void YouSureUnsaved(bool yes) {
        string origCat, origDesc;
        origCat = originalCategory.category;
        origDesc = originalCategory.description;
        Debug.Log(category == origDesc);

        if (!wereChangesMade){
            sceneLoader.LoadLastScene();
            return;
        }
        UnsavedUI.SetActive(yes);
    }

    public void DeleteFile() {
        string filePath = Application.persistentDataPath + "/customCategories/" + origFileName + ".json";
        File.Delete(filePath);
        sceneLoader.LoadLastScene();
    }

}
