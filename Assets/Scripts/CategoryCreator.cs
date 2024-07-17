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

    // Editor mode related
    public static Category originalCategory;
    public string origFileName;
    public bool wereChangesMade = false;
    public static bool isInitializing = true;

    // Start is called before the first frame update
    void Start()
    {
        //if (questions == null)
        InitializeValues();
    }

    public void InitializeValues()
    {
        questions = new List<string>();
        Object[] iconArray = Resources.LoadAll("", typeof(Sprite));
        List<TMP_Dropdown.OptionData> iconList = new();
        switch (Config.creatingCategory)
        {
            case true:
                category = "";
                description = "";
                foreach (Object icon in iconArray)
                {
                    TMP_Dropdown.OptionData od = new TMP_Dropdown.OptionData(icon.name, (Sprite)icon);
                    iconList.Add(od);
                }
                iconDropdown.options = iconList;
                warningsRunning = 0;
                break;
            case false:
                isInitializing = true;
                category = originalCategory.category;
                description = originalCategory.description;
                string iconToSet = null;
                foreach (Object icon in iconArray)
                {
                    TMP_Dropdown.OptionData od = new TMP_Dropdown.OptionData(icon.name, (Sprite)icon);
                    iconList.Add(od);
                    if (icon.name == originalCategory.iconName)
                    {
                        iconToSet = icon.name;
                    }
                }
                category_text.transform.parent.transform.parent.GetComponent<TMP_InputField>().text = originalCategory.category;
                desc_text.transform.parent.transform.parent.GetComponent<TMP_InputField>().text = originalCategory.description;
                DeleteablePromptController.canDelete = true;
                foreach (string s in originalCategory.questions)
                {
                    AddToQuestionList(s);
                }
                origFileName = originalCategory.category.Replace(' ', '_');
                // TBA: put option in opened file as selected option
                iconDropdown.options = iconList;
                if (iconToSet != null)
                    SetImage(iconToSet);
                wereChangesMade = false;
                isInitializing = false;
                break;
        }
    }
    public void SetImage(string name)
    {
        iconDropdown.value = iconDropdown.options.FindIndex(option => option.text == name);
    }
    public void AddQuestion()
    {
        string newQuestion = question_text.text;
        if (newQuestion.Length > 1)
        {
            wereChangesMade = true;
            AddToQuestionList(newQuestion);
        }
        else
            StartCoroutine(ShowWarningText("No se puede agregar un enunciado vacío."));
        promptInputField.text = "";
    }

    public void RemoveQuestion(string prompt)
    {
        if (questions.Contains(prompt))
        {
            questions.Remove(prompt);
            wereChangesMade = true;
        }
        else
            StartCoroutine(ShowWarningText("No hay un enunciado \"" + prompt + "\" en esta categoría."));
    }

    public void AddToQuestionList(string prompt)
    {
        if (questions.Contains(prompt))
        {
            StartCoroutine(ShowWarningText("Este enunciado ya fue agregado."));
            return;
        }
        questions.Add(prompt);
        Debug.Log("Question: \"" + prompt + "\" added.");
        GameObject newQuestion = Instantiate(promptPrefab, promptParent.transform);
        newQuestion.GetComponentInChildren<DeleteablePrompt>().SetValue(prompt); // set name;
        newQuestion.GetComponentInChildren<DeleteablePrompt>().EnableDeleting(!DeleteablePromptController.canDelete); // set cross on/off
    }

    public void SetName()
    {
        category = category_text.text;
    }

    public void SetDescription()
    {
        description = desc_text.text;
    }

    public bool isCategoryAllowed()
    {
        // check custom category names
        if (category.Length <= 1)
        {
            StartCoroutine(ShowWarningText("Debes ponerle un nombre a la categoría."));
            return false;
        }
        if (questions.Count == 0)
        {
            StartCoroutine(ShowWarningText("La categoría no tiene suficientes enunciados."));
            return false;
        }
        if (description.Length <= 1)
        {
            StartCoroutine(ShowWarningText("La categoría no tiene una descripción."));
            return false;
        }
        return true;
    }

    public static void CreateCustomDirectory(string savePath)
    {

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
    }

    // Creates json file of the new category.
    public void CreateFile()
    {
        if (!isCategoryAllowed())
        {
            failSound.PlayClip();
            return;
        }
        successSound.PlayClip();
        string icon = iconDropdown.captionImage.sprite.name;
        Debug.Log("category: " + category + " description: " + description + " icon: " + icon + "\nquestions" + questions);
        Category newCustomCategory = new Category()
        {
            category = this.category,
            description = this.description,
            iconName = icon,
            questions = this.questions
        };
        string categoryString = JsonUtility.ToJson(newCustomCategory);
        string savePath = Application.persistentDataPath + "/customCategories";
        CreateCustomDirectory(savePath);
        string origPath = savePath;
        savePath += "/" + category.Replace(' ', '_') + ".json";
        Debug.Log(savePath);
        if (Config.creatingCategory)
            origPath += "/" + origFileName + ".json";
        else
            origPath = savePath;
        using StreamWriter streamWriter = new StreamWriter(origPath);
        streamWriter.Write(categoryString);
        streamWriter.Close();
        if (Config.creatingCategory)
            File.Move(origPath, savePath);
        sceneLoader.LoadLastScene();
    }

    // Used to delete a category's json file when it is removed.
    public void DeleteFile()
    {
        string filePath = Application.persistentDataPath + "/customCategories/" + origFileName + ".json";
        File.Delete(filePath);
        sceneLoader.LoadLastScene();
    }

    IEnumerator ShowWarningText(string warning)
    {
        warningsRunning++;
        warning_text.enabled = true;
        warning_text.text = warning;
        yield return new WaitForSeconds(3f);
        warningsRunning--;
        if (warningsRunning == 0)
            warning_text.enabled = false;
    }

    // Warns player about deleting data
    public void YouSure(bool yes)
    {
        DeleteUI.SetActive(yes);
    }

    // Warns player about unsaved changes if needed; if not, loads last scene
    public void YouSureUnsaved(bool yes)
    {
        if (NoNeedToAsk())
        {
            sceneLoader.LoadLastScene();
            return;
        }
        UnsavedUI.SetActive(yes);
    }

    // Checks if it should ask the player if they're sure about leaving without changes
    private bool NoNeedToAsk()
    {
        switch (Config.creatingCategory)
        {
            case false:
                return ((questions.Count == 0) && (description.Length <= 1) && (category.Length <= 1));
            case true:
                return (!wereChangesMade);

        }
    }

    public void SetChangesWereMade(bool wereChangesMade)
    {
        this.wereChangesMade = wereChangesMade;
    }
}
