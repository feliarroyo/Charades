using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Prompt pool used in Mash-Up mode.
/// </summary>
public class MashUpPool : IPromptPool
{
    public static Category currentCategory;
    public static Dictionary<string, List<string> > mashupPrompts;
    public static Category currentMashUpCategory;
    
    public void InitializePrompts()
    {
        mashupPrompts = new();
        foreach (Category c in Competition.categories)
            mashupPrompts[c.title] = Competition.GetPrompts(c);
    }

    public void RemovePrompt(string promptText)
    {
        mashupPrompts[currentMashUpCategory.title].Remove(promptText);
    }

    public string GetNewPrompt()
    {
        return mashupPrompts[currentCategory.title][Random.Range(0, mashupPrompts[currentCategory.title].Count)];
    }

    public void RecordUsedPrompts()
    {
        foreach (string c in mashupPrompts.Keys){
            Competition.sessionCategories[c] = mashupPrompts[c];
        }
    }

    public void RestartPoolIfEmpty()
    {
        currentMashUpCategory = Competition.categories[Random.Range(0, Competition.categories.Count)];
        currentCategory = currentMashUpCategory;
        if (mashupPrompts[currentCategory.title].Count == 0){
            mashupPrompts[currentCategory.title] = new(currentMashUpCategory.questions);
        }   
    }
}
