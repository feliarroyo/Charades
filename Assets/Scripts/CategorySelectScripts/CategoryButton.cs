using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

/// <summary>
/// This class contains the behavior related to the buttons used for each category.
/// </summary>
public class CategoryButton : MonoBehaviour
{
    public TextMeshProUGUI categoryName; // text space for the name
    public Button buttonComponent; // button image
    public TextAsset jsonCategory; // json file containing all information
    public string jsonCategoryName;
    public Image categoryImage; // category symbol on the button
    protected Category category;
    protected bool singleSelect;
    protected ColorBlock unselectedColor;
    protected ColorBlock selectedColor;

    // Competition Position Mark
    public GameObject mark;
    public TextMeshProUGUI markText;
    protected int position;
    public string customFilePath = "";

    // Start is called before the first frame update
    async void Start()
    {
        await LocalizationSettings.InitializationOperation.Task;
        LoadCategoryInfo();
        // Define colors for different states
        unselectedColor = SetColor(unselectedColor, Color.white);
        selectedColor = SetColor(selectedColor, Color.green);
        // Initialize color value depending on previous game state and game type
        if (Competition.ContainsCategory(category) && (Competition.gameType != Const.GameModes.QuickPlay))
        {
            SetCategory_selected();
        }
        else
        {
            buttonComponent.colors = unselectedColor;
        }
        Debug.Log("At the end, JSON is now: " + jsonCategory.name);
    }

    protected virtual void LoadCategoryInfo(){
        category = CategoryDatabase.Get(jsonCategoryName);
        //category = JSONReader.GetCategory(jsonCategory);
        categoryName.text = category.title;
        Sprite categorySprite = Resources.Load<Sprite>(category.iconName);
        categoryImage.sprite = Resources.Load<Sprite>(categorySprite == null? "default" : category.iconName);
        
    }
    
    public void SetCategory() {
        if (!singleSelect){
            bool isBeingAdded = Competition.AddCategory(category);
            if (Competition.gameType == Const.GameModes.Competition){
                mark.SetActive(isBeingAdded);
                if (isBeingAdded){
                    position = Competition.GetCurrentCategoryPosition();
                    markText.text = position.ToString();
                    Debug.Log(category.title + " tiene posición " + position);
                    CategorySelect.selectedCatButtons.Add(this);
                    Debug.Log("Hay seleccionadas #cat:" + CategorySelect.selectedCatButtons.Count);
                }
                else {
                    CategorySelect.selectedCatButtons.Remove(this);
                    for (int i = position-1; i < CategorySelect.selectedCatButtons.Count; i++){
                        Debug.Log("I iteration:" + i + " Position " + position);
                        CategoryButton cb = CategorySelect.selectedCatButtons[i];
                        cb.position--;
                        cb.markText.text = cb.position.ToString();
                    }
                }
            }
        }
        switch (Competition.gameType){
            case Const.GameModes.QuickPlay:
                Competition.StartCompetition();
                SceneManager.LoadScene(Const.SCENE_PRESENT);
                return;
            case Const.GameModes.Competition:
                break;
            default:
                break;
        }
        ChangeButtonColors();
    }

    /// <summary>
    /// Change the color of the button, from selected to unselected and vice versa.
    /// </summary>
    private void ChangeButtonColors(){
        if (buttonComponent.colors.Equals(unselectedColor))
            buttonComponent.colors = selectedColor;
        else
            buttonComponent.colors = unselectedColor;
    }

    public void SetCategoryAsEdit() {
        CategoryCreator.originalCategory = category;
        CategoryCreator.origFilePath = customFilePath;
    }

    public void SetSingleSelect(){
        singleSelect = true;
    }

    public void SetCategory_unselected(){
        CategorySelect.selectedCatButtons.Remove(this);
        buttonComponent.colors = unselectedColor;
        mark.SetActive(false);
    }

    public void SetCategory_selected(){
        buttonComponent.colors = selectedColor;
        if (Competition.gameType == Const.GameModes.Competition) {
            mark.SetActive(true);
            position = Competition.GetCategoryPosition(category)+1;
            Debug.Log(category.title + " Position:" + position);
            CategorySelect.selectedCatButtons[position-1]=this;
            markText.text = (Competition.GetCategoryPosition(category) + 1).ToString();
        }
    }

    public void RemoveCategory(){
        Competition.RemoveCategory(category);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cb"></param>
    /// <param name="color"></param>
    private ColorBlock SetColor(ColorBlock cb, Color color){
        cb = buttonComponent.colors;
        cb.normalColor = color;
        cb.selectedColor = color;
        return cb;
    }

    public string GetFileName(){
        return category.title.Replace(' ', '_');
    }
}
