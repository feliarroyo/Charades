using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;

/// <summary>
/// This class contains everything related to the creation of categories, and their modification in the Category Editor.
/// </summary>
public class CategoryCreator : MonoBehaviour
{
    public static CategoryCreator creator; // Singleton
    
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI PromptText;
    public TextMeshProUGUI WarningText;
    public TextMeshProUGUI PromptCounter_Main;
    public TextMeshProUGUI PromptCounter_List;
    public static string iconName;
    public static Image iconImage;
    protected string categoryTitle;
    protected string description;
    protected List<string> questions;
    public SceneLoader sceneLoader;
    public SoundEffectPlayer successSound;
    public SoundEffectPlayer  failSound;
    public GameObject promptPrefab;
    public GameObject promptParent;
    public GameObject iconButtonPrefab;
    public GameObject iconParent; // GameObject in which icon buttons are placed as children.
    public static int warningsRunning = 0;
    public GameObject UnsavedUI;
    public GameObject PromptUI;
    public GameObject IconUI;
    public TMP_InputField promptInputField;

    // Editor mode related
    public static Category originalCategory;
    public static string origFilePath;
    public bool wereChangesMade = false;
    public static bool changeCanvas = false;

    // Warnings
    private const string
        WARNING_EMPTYPROMPT = "No se puede agregar un enunciado vacío.",
        WARNING_EMPTYPROMPT_EN = "Empty prompts cannot be added.",
        WARNING_REPEATPROMPT = "Este enunciado ya fue agregado.",
        WARNING_REPEATPROMPT_EN = "This prompt has already been added.",
        WARNING_EMPTYTITLE = "Debes ponerle un nombre a la categoría.",
        WARNING_EMPTYTITLE_EN = "The category must be named.",
        WARNING_ZEROPROMPTS = "La categoría no tiene suficientes enunciados.",
        WARNING_ZEROPROMPTS_EN = "The category does not have enough prompts.",
        WARNING_EMPTYDESC = "La categoría no tiene una descripción.",
        WARNING_EMPTYDESC_EN = "The category must have a description."
    ;

    // Start is called before the first frame update
    void Start()
    {
        creator = this;
        InitializeValues();
        IconInstantiate();
    }

    void Update(){
        if (changeCanvas)
            ShowIconCanvas(false);
        changeCanvas = false;
    }

    /// <summary>
    /// Sets icon selected, and adds an icon button in the icon select sub-screen for every icon available.
    /// </summary>
    private void IconInstantiate()
    {
        UnityEngine.Object[] iconArray = Resources.LoadAll("", typeof(Sprite));
        iconName = Const.DEFAULT_ICON;
        foreach (UnityEngine.Object icon in iconArray)
            {
                if ((originalCategory != null) && (icon.name == originalCategory.iconName)) {
                    SetImage(icon.name, (Sprite) icon);
                }
                GameObject newIconButton = Instantiate(iconButtonPrefab, iconParent.transform);
                newIconButton.GetComponent<IconButton>().SetSprite(icon.name, (Sprite)icon);
            }
    }

    public void InitializeValues()
    {
        iconImage = GameObject.Find("iconImage").GetComponent<Image>();
        questions = new();
        UnityEngine.Object[] iconArray = Resources.LoadAll("", typeof(Sprite));
        warningsRunning = 0;
        DeleteablePromptController.canDelete = false;
        switch (Config.creatingNewCategory)
        {
            case true:
                categoryTitle = "";
                description = "";
                PromptCounter_Main.text = "0";
                PromptCounter_List.text = "0";
                break;
            case false:
                categoryTitle = SetText(originalCategory.title, titleText);
                description = SetText(originalCategory.description, descriptionText);
                AddToQuestionList(originalCategory.questions);
                // TBA: put option in opened file as selected option
                wereChangesMade = false;
                break;
        }
    }

    private string SetText(string textSource, TextMeshProUGUI textGO){
        textGO.transform.parent.transform.parent.GetComponent<TMP_InputField>().text = textSource;
        return textSource;
    }

    public static void SetImage(string name, Image image)
    {
        iconName = name;
        iconImage.sprite = image.sprite;
    }

    public static void SetImage(string name, Sprite sprite)
    {
        iconName = name;
        iconImage.sprite = sprite;
    }

    public void AddQuestion()
    {
        string newQuestion = PromptText.text;
        if (newQuestion.Length > 1)
        {
            wereChangesMade = true;
            AddToQuestionList(newQuestion);
        }
        else
            StartCoroutine(ShowWarningText(Const.EnglishLocaleActive() ? WARNING_EMPTYPROMPT_EN : WARNING_EMPTYPROMPT));
        promptInputField.text = "";
    }

    public void RemoveQuestion(string prompt)
    {
        if (questions.Contains(prompt))
        {
            questions.Remove(prompt);
            wereChangesMade = true;
            PromptCounter_Main.text = questions.Count.ToString();
            PromptCounter_List.text = questions.Count.ToString();
        }
    }

    /// <summary>
    /// Adds the prompt passed as a parameter to the category, as long as it's not a repeated one.
    /// </summary>
    /// <param name="prompt">Prompt to be added to the category.</param>
    public void AddToQuestionList(string prompt)
    {
        if (questions.Contains(prompt))
        {
            StartCoroutine(ShowWarningText(Const.EnglishLocaleActive() ? WARNING_REPEATPROMPT_EN : WARNING_REPEATPROMPT));
            return;
        }
        questions.Add(prompt);
        PromptCounter_Main.text = questions.Count.ToString();
        GameObject newQuestion = Instantiate(promptPrefab, promptParent.transform);
        newQuestion.GetComponentInChildren<DeleteablePrompt>().SetValue(prompt); // set name;
        newQuestion.GetComponentInChildren<DeleteablePrompt>().EnableDeleting(DeleteablePromptController.canDelete); // set cross on/off
    }

    /// <summary>
    /// Adds the content of an entire list to the prompt list.
    /// </summary>
    /// <param name="promptList">List of strings to be added as prompts.</param>
    public void AddToQuestionList(List<string> promptList){
        questions.AddRange(promptList);
        PromptCounter_Main.text = questions.Count.ToString();
        foreach (string prompt in questions){
            GameObject newQuestion = Instantiate(promptPrefab, promptParent.transform);
            newQuestion.GetComponentInChildren<DeleteablePrompt>().SetValue(prompt); // set name;
            newQuestion.GetComponentInChildren<DeleteablePrompt>().EnableDeleting(DeleteablePromptController.canDelete); // set cross on/off
        }
    }

    /// <summary>
    /// Changes the category name according to what is written in the corresponding text space.
    /// </summary>
    public void SetName()
    {
        categoryTitle = titleText.text;
        wereChangesMade = true;
    }

    /// <summary>
    /// Changes description according to what is written in the corresponding text space.
    /// </summary>
    public void SetDescription()
    {
        description = descriptionText.text;
        wereChangesMade = true;
    }

    /// <summary>
    /// Determines if the category meets the requirements to be allowed: Having a title, a description and at least a prompt. 
    /// If not, informs about it on screen.
    /// </summary>
    /// <returns>Returns true if the category is allowed to be saved.</returns>
    public bool IsCategoryAllowed()
    {
        // check custom category names
        if (categoryTitle.Length <= 1)
        {
            StartCoroutine(ShowWarningText(Const.EnglishLocaleActive() ? WARNING_EMPTYTITLE_EN : WARNING_EMPTYTITLE));
            return false;
        }
        if (questions.Count == 0)
        {
            StartCoroutine(ShowWarningText(Const.EnglishLocaleActive() ? WARNING_ZEROPROMPTS_EN : WARNING_ZEROPROMPTS));
            return false;
        }
        if (description.Length <= 1)
        {
            StartCoroutine(ShowWarningText(Const.EnglishLocaleActive() ? WARNING_EMPTYDESC_EN : WARNING_EMPTYDESC));
            return false;
        }
        return true;
    }

    /// <summary>
    /// If not previously created, creates a folder to store custom categories.
    /// </summary>
    /// <param name="savePath"></param>
    public static void CreateCustomDirectory()
    {

        if (!Directory.Exists(Const.customDirectory))
        {
            Directory.CreateDirectory(Const.customDirectory);
        }
    }

    /// <summary>
    /// Creates a .JSON file of the new category, and returns to the last scene opened if successful.
    /// </summary>
    public void CreateFile()
    {
        if (!IsCategoryAllowed())
        {
            failSound.PlayClip();
            return;
        }
        successSound.PlayClip();
        Category newCustomCategory = new() { title = categoryTitle, description = description, iconName = iconName, questions = questions };
        string categoryString = JsonUtility.ToJson(newCustomCategory);
        CreateCustomDirectory();
        string origPath;
        string savePath = Const.customDirectory + "/" + categoryTitle.Replace(' ', '_') + ".json";
        if (!Config.creatingNewCategory)
            origPath = origFilePath;
        else
            origPath = savePath;
        using StreamWriter streamWriter = new(origPath);
        streamWriter.Write(categoryString);
        streamWriter.Close();
        if (!Config.creatingNewCategory)
            File.Move(origPath, savePath);
        sceneLoader.LoadLastScene();
    }

    // Used to delete a category's json file when it is removed.
    public void DeleteFile()
    {
        Competition.RemoveCategory(originalCategory);
        File.Delete(origFilePath);
        sceneLoader.LoadLastScene();
    }

    IEnumerator ShowWarningText(string warning)
    {
        warningsRunning++;
        WarningText.enabled = true;
        WarningText.text = warning;
        yield return new WaitForSeconds(3f);
        warningsRunning--;
        if (warningsRunning == 0)
            WarningText.enabled = false;
    }

    /// <summary>
    /// Warns player about unsaved changes if needed; if not, loads last scene
    /// </summary>
    /// <param name="yes"></param>
    public void YouSureUnsaved(bool yes)
    {
        if (NoNeedToAsk())
        {
            sceneLoader.LoadLastScene();
            return;
        }
        UnsavedUI.SetActive(yes);
    }

    public void ShowCanvas(bool setActive){
        PromptUI.SetActive(setActive);
        PromptCounter_Main.text = questions.Count.ToString();
        PromptCounter_List.text = questions.Count.ToString();
    }

    /// <summary>
    /// Enables/Disables the icon selection screen.
    /// </summary>
    /// <param name="setActive"></param>
    public void ShowIconCanvas(bool setActive){
        IconUI.SetActive(setActive);
    }

    public void ExportFiles(){
        string filePath = origFilePath;
        //NativeFilePicker.Permission permission = NativeFilePicker.ExportFile( filePath, ( success ) => Debug.Log( "File exported: " + success ) );
        new NativeShare().AddFile(filePath).SetSubject( "Charadas - Categoría personalizada").SetText("Puedes agregar la categoría a Charadas en la sección de Crear categorías, pulsando el botón de Importar categoría y seleccionando el archivo.").Share();
    }

    /// <summary>
    /// Checks if it should ask the player if they're sure about leaving without changes.
    /// </summary>
    /// <returns>True if there's no need to show a popup about leaving the category creator.</returns>
    private bool NoNeedToAsk()
    {
        return Config.creatingNewCategory? ((questions.Count == 0) && (description.Length <= 1) && (categoryTitle.Length <= 1))
            : !wereChangesMade;
    }

    /// <summary>
    /// Used to indicate if changes were made from the original data in the category.
    /// </summary>
    public void SetChangesWereMade(bool wereChangesMade)
    {
        this.wereChangesMade = wereChangesMade;
    }
}