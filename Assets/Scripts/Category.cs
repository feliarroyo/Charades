using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Category : IEquatable<Category>
{
    public string category;
    public string description;
    public List<string> questions;
    
    public bool Equals(Category c)
    {
    if (c == null)
        return false;
    return c.category.Equals(category);
    }
}
