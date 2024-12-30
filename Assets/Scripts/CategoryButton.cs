using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using System;
using JetBrains.Annotations;

public class CategoryButton : MonoBehaviour
{
    public TextMeshProUGUI categoryName;
    public Button buttonImage;
    public TextAsset jsonCategory;
    public Image categoryImage;
    private Category category;
    private bool singleSelect;
    private ColorBlock unselectedColor, selectedColor;

    // Competition Position Mark
    public GameObject mark;
    public TextMeshProUGUI markText;
    private int position;
    private static readonly List<CategoryButton> selectedCatButtons = new();

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
        if (Competition.ContainsCategory(category) && (Competition.gameType != Const.GameModes.QuickPlay))
            SetCategory_selected();
        else
            buttonImage.colors = unselectedColor;
        
    }

    public void SetCategory() {
        if (!singleSelect){
            bool isBeingAdded = Competition.AddCategory(category);
            if (Competition.gameType == Const.GameModes.Competition){
                mark.SetActive(isBeingAdded);
                if (isBeingAdded){
                    position = Competition.GetCurrentCategoryPosition();
                    markText.text = position.ToString();
                    Debug.Log(category.category + " tiene posición " + position);
                    selectedCatButtons.Add(this);
                    Debug.Log("Hay seleccionadas #cat:" + selectedCatButtons.Count);
                }
                else {
                    selectedCatButtons.Remove(this);
                    for (int i = position-1; i < selectedCatButtons.Count; i++){
                        CategoryButton cb = selectedCatButtons[i];
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

    private void ChangeButtonColors(){
        if (buttonImage.colors.Equals(unselectedColor))
            buttonImage.colors = selectedColor;
        else
            buttonImage.colors = unselectedColor;
    }

    public void SetCategoryAsEdit() {
        CategoryCreator.originalCategory = this.category;
    }

    public void SetSingleSelect(){
        singleSelect = true;
    }

    public void SetCategory_unselected(){
        selectedCatButtons.Remove(this);
        buttonImage.colors = unselectedColor;
        mark.SetActive(false);
    }

    public void SetCategory_selected(){
        buttonImage.colors = selectedColor;
        if (Competition.gameType == Const.GameModes.Competition) {
            mark.SetActive(true);
            markText.text = (Competition.GetCategoryPosition(category) + 1).ToString();
        }
    }

    private ColorBlock SetColor(ColorBlock cb, Color color){
        cb = buttonImage.colors;
        cb.normalColor = color;
        cb.selectedColor = color;
        //cb.highlightedColor = color;
        return cb;
    }
}
