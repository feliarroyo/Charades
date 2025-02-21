using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPromptPool
{
    /// <summary>
    /// Starts up prompt pool to use during the round.
    /// </summary>
    public void InitializePrompts();
    /// <summary>
    /// Removes prompt from the pool, to minimize repeated prompts during game sessions.
    /// </summary>
    public void RemovePrompt(string promptText);
    /// <summary>
    /// Restart pool, in case there are no other new prompts to use.
    /// </summary>
    public void RestartPoolIfEmpty();
    /// <summary>
    /// Returns a new prompt from the pool in string form.
    /// </summary>
    /// <returns>String that contains a new prompt.</returns>
    public string GetNewPrompt();
    /// <summary>
    /// Stores prompt pools as they are after the round, to prevent repeat prompts in future rounds.
    /// </summary>
    public void RecordUsedPrompts();

}
