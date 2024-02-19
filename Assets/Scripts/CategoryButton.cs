using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    public TextMeshProUGUI categoryName;
    public Button buttonImage;
    public TextAsset jsonCategory;
    private Category category;
    private ColorBlock unselectedColor, selectedColor;
    // Start is called before the first frame update
    void Start()
    {
        category = JSONReader.GetCategory(jsonCategory);
        categoryName.text = category.category;
        unselectedColor = SetColor(unselectedColor, Color.white);
        selectedColor = SetColor(selectedColor, Color.green);
        if (Competition.ContainsCategory(category))
            buttonImage.colors = selectedColor;
        else
            buttonImage.colors = unselectedColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCategory() {
        Competition.AddCategory(category);
        if (buttonImage.colors.Equals(unselectedColor))
            buttonImage.colors = selectedColor;
        else
            buttonImage.colors = unselectedColor;
    }

    public void SetCategory_unselected(){
        buttonImage.colors = unselectedColor;
    }

    private ColorBlock SetColor(ColorBlock cb, Color color){
        cb = buttonImage.colors;
        cb.normalColor = color;
        cb.selectedColor = color;
        cb.highlightedColor = color;
        return cb;
    }
}
