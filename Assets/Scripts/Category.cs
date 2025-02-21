using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Category : IEquatable<Category>
{
    public string title;
    public string description;
    public List<string> questions;
    public string iconName;
    
    public bool Equals(Category c) {
        if (c == null)
            return false;
        return c.title.Equals(title);
    }
}
