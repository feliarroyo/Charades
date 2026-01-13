using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

/// <summary>
/// This class contains the behavior related to the buttons used for each category.
/// </summary>
public class CategoryButton_Custom : CategoryButton
{

    protected override void LoadCategoryInfo()
    {
        category = JSONReader.GetCategory(jsonCategory);
        categoryName.text = category.title;
        Sprite categorySprite = Resources.Load<Sprite>(category.iconName);
        categoryImage.sprite = Resources.Load<Sprite>(categorySprite == null ? "default" : category.iconName);
    }
}
