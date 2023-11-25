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
    private ColorBlock unselected, selected;
    // Start is called before the first frame update
    void Start()
    {
        category = JSONReader.GetCategory(jsonCategory);
        categoryName.text = category.category;
        unselected = SetColor(unselected, Color.white);
        selected = SetColor(selected, Color.green);
        if (Competition.ContainsCategory(category))
            buttonImage.colors = selected;
        else
            buttonImage.colors = unselected;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCategory() {
        Competition.AddCategory(category);
        if (buttonImage.colors.Equals(unselected))
            buttonImage.colors = selected;
        else
            buttonImage.colors = unselected;
    }

    private ColorBlock SetColor(ColorBlock cb, Color color){
        cb = buttonImage.colors;
        cb.normalColor = color;
        cb.selectedColor = color;
        cb.highlightedColor = color;
        return cb;
    }
}
