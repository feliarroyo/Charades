using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    public string iconName;
    public Image iconImage;

    public void SetSprite(string iconName, Sprite icon){
        this.iconName = iconName;
        iconImage.sprite = icon;
    }

    public void UseSprite(){
        CategoryCreator.SetImage(iconName, iconImage);
        CategoryCreator.changeCanvas = true;
    }

    public bool Equals(Icon i) {
        if (i == null)
            return false;
        return i.iconName.Equals(iconName);
    }
}
