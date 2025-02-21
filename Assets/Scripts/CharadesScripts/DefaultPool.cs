using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DefaultPool : IPromptPool
{
    public static Category currentCategory;
    public static List<string> prompts; // Contains prompts used during the current round in non-mashup modes
    
    public void InitializePrompts()
    {
        currentCategory = Competition.GetCategory();
        prompts = Competition.GetPrompts(currentCategory);
    }
    
    public void RemovePrompt(string promptText)
    {
        prompts.Remove(promptText);
    }

    public string GetNewPrompt()
    {
        return prompts[Random.Range(0, prompts.Count)];
    }

    public void RecordUsedPrompts()
    {
        Competition.sessionCategories[currentCategory.title] = prompts;
    }

    public void RestartPoolIfEmpty()
    {
        if (prompts.Count == 0)
            prompts = new(currentCategory.questions);
    }
}
