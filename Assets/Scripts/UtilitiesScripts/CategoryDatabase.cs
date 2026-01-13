using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CategoryDatabase
{
    private static Dictionary<string, Category> categories;

    public static void LoadAll()
    {
        if (categories != null) return;

        categories = new Dictionary<string, Category>();

        var files = Resources.LoadAll<TextAsset>("Categories");

        foreach (var file in files)
        {
            var data = JsonUtility.FromJson<Category>(file.text);
            categories[file.name] = data;
        }
    }

    public static Category Get(string id)
    {
        return categories[Const.EnglishLocaleActive() ? id + "_en" : id];
    }
}
