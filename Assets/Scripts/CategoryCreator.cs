using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class CategoryCreator : MonoBehaviour
{
    public TextMeshProUGUI category_text, desc_text, question_text, warning_text, questionlist_text;
    public TMP_Dropdown iconDropdown;
    protected string category, description;
    protected List<string> questions;
    public SceneLoader sceneLoader;
    public SoundEffectPlayer successSound, failSound;
    public GameObject promptPrefab;
    public GameObject promptParent;
    public static int warningsRunning = 0;
    public GameObject UnsavedUI, DeleteUI;
    public TMP_InputField promptInputField;

    // Start is called before the first frame update
    void Start()
    {
        //if (questions == null)
            InitializeValues();
    }

    public void InitializeValues(){
        category = "";
        description = "";
        questions = new List<string>();
        Object[] iconArray = Resources.LoadAll("", typeof(Sprite));
        List<TMP_Dropdown.OptionData> iconList = new();
        foreach (Object icon in iconArray) {
            TMP_Dropdown.OptionData od = new TMP_Dropdown.OptionData(icon.name, (Sprite) icon);
            iconList.Add(od);
        }
        iconDropdown.options = iconList;
        warningsRunning = 0;
    }

    public void SetImage(string name){
        iconDropdown.value = iconDropdown.options.FindIndex(option => option.text == name);
    }
    public void AddQuestion(){
        string newQuestion = question_text.text;
        if (newQuestion.Length > 1) // Questions can't be empty, also add verification later
            AddToQuestionList(newQuestion);
        else
            StartCoroutine(ShowWarningText("No se puede agregar un enunciado vacío."));
        promptInputField.text = "";
    }

    public void RemoveQuestion(string prompt){
        if (questions.Contains(prompt))
            questions.Remove(prompt);
        else
            StartCoroutine(ShowWarningText("No hay un enunciado \"" + prompt + "\" en esta categoría."));
    }

    public void AddToQuestionList(string prompt){
        if (questions.Contains(prompt)){
            StartCoroutine(ShowWarningText("Este enunciado ya fue agregado."));
            return;
        }
        questions.Add(prompt);
        Debug.Log("Question: \"" + prompt + "\" added.");
        GameObject newQuestion = Instantiate(promptPrefab, promptParent.transform);
        newQuestion.GetComponentInChildren<DeleteablePrompt>().SetValue(prompt); // set name;
        newQuestion.GetComponentInChildren<DeleteablePrompt>().EnableDeleting(!DeleteablePromptController.canDelete); // set cross on/off
    }

    public void SetName(){
        category = category_text.text;
    }

    public void SetDescription(){
        description = desc_text.text;
    }

    public bool isCategoryAllowed(){
        // check custom category names
        if (category.Length <= 1){
            StartCoroutine(ShowWarningText("Debes ponerle un nombre a la categoría."));
            return false;
        }
        if (questions.Count == 0){
            StartCoroutine(ShowWarningText("La categoría no tiene suficientes enunciados."));
            return false;
        }
        if (description.Length <= 1){
            StartCoroutine(ShowWarningText("La categoría no tiene una descripción."));
            return false;
        }
        return true;
    }

    public static void CreateCustomDirectory(string savePath){
        
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
    }

    // Creates json file of the new category.
    public void CreateFile(){
        if (!isCategoryAllowed()){
            failSound.PlayClip();
            return;
        }
        successSound.PlayClip();
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
        savePath += "/" + category.Replace(' ', '_') + ".json";
        Debug.Log(savePath);
        using StreamWriter streamWriter = new StreamWriter(savePath);
        streamWriter.Write(categoryString);
        sceneLoader.LoadLastScene();
    }

    IEnumerator ShowWarningText(string warning){
        warningsRunning++;
        warning_text.enabled = true;
        warning_text.text = warning;
        yield return new WaitForSeconds(3f);
        warningsRunning--;
        if (warningsRunning == 0)
            warning_text.enabled = false;
    }

    public void YouSure(bool yes) {
        DeleteUI.SetActive(yes);
    }
    public void YouSureUnsaved(bool yes) {
        if ((questions.Count == 0) && (description.Length <= 1) && (category.Length <= 1)){
            sceneLoader.LoadLastScene();
            return;
        }
        UnsavedUI.SetActive(yes);
    }
}
