using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used to identify icons within each Icon Button in the category creator.
/// </summary>
public class IconButton : MonoBehaviour
{
    public string iconName; // internal name, used in a category's .JSON file and to identify icons.
    public Image iconImage;

    /// <summary>
    /// Initializes the icon button's icon name and image.
    /// </summary>
    /// <param name="iconName">internal name that identifies the icon.</param>
    /// <param name="icon">The icon's image.</param>
    public void SetSprite(string iconName, Sprite icon){
        this.iconName = iconName;
        iconImage.sprite = icon;
    }

    /// <summary>
    /// Sets this icon as the one to be used in the Custom Category Creator, 
    /// and returns to the main screen in the Creator.
    /// </summary>
    public void UseSprite(){
        CategoryCreator.SetImage(iconName, iconImage);
        CategoryCreator.creator.ShowIconCanvas(false);
    }

    /// <summary>
    /// Used to allow icon comparisons according to the internal icon name.
    /// </summary>
    /// <param name="i">Icon to be compared with.</param>
    /// <returns>True if it is an icon with the same internal name.</returns>
    public bool Equals(IconButton i) {
        if (i == null)
            return false;
        return i.iconName.Equals(iconName);
    }
}
