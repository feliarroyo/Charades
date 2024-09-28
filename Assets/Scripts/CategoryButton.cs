using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CategoryButton : MonoBehaviour
{
    public TextMeshProUGUI categoryName;
    public Button buttonImage;
    public TextAsset jsonCategory;
    public Image categoryImage;
    private Category category;
    private bool singleSelect;
    private ColorBlock unselectedColor, selectedColor;
    // Start is called before the first frame update
    void Start()
    {
        // Loads category data from file
        category = JSONReader.GetCategory(jsonCategory);
        categoryName.text = category.category;
        categoryImage.sprite = Resources.Load<Sprite>(category.iconName);
        // Define colors for different states
        unselectedColor = SetColor(unselectedColor, Color.white);
        selectedColor = SetColor(selectedColor, Color.green);
        // Initialize color value depending on previous game state and game type
        if ((Competition.ContainsCategory(category)) && (Competition.gameType != 0))
            buttonImage.colors = selectedColor;
        else
            buttonImage.colors = unselectedColor;
        
    }

    public void SetCategory() {
        if (!singleSelect)
            Competition.AddCategory(category);
        switch (Competition.gameType){
            case 0:
                Competition.StartCompetition();
                SceneManager.LoadScene(Const.SCENE_PRESENT);
                break;
            default:
                if (buttonImage.colors.Equals(unselectedColor))
                    buttonImage.colors = selectedColor;
                else
                    buttonImage.colors = unselectedColor;
                break;
        }
    }

    public void SetCategoryAsEdit() {
        CategoryCreator.originalCategory = this.category;
    }

    public void SetSingleSelect(){
        singleSelect = true;
    }

    public void SetCategory_unselected(){
        buttonImage.colors = unselectedColor;
    }

    private ColorBlock SetColor(ColorBlock cb, Color color){
        cb = buttonImage.colors;
        cb.normalColor = color;
        cb.selectedColor = color;
        //cb.highlightedColor = color;
        return cb;
    }
}
