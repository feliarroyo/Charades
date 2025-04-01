using UnityEngine;

/// <summary>
/// This class is used to determine if a button that transitions to a different menu
/// should load a variant within the scene.
/// Examples of such variants: Gameplay Category Select vs. Custom Category Select; 
/// Custom Category Creator vs. Custom Category Editor.
/// </summary>
public class TriggerMenuVariant : MonoBehaviour
{
    public bool triggersVariant = false;

    /// <summary>
    /// Used to define if the category select menu should be set up to choose categories to play
    /// or to choose custom categories to edit.
    /// </summary>
                
    public void SetCurrentMode(){
        Config.customMenu = triggersVariant;
    }

    /// <summary>
    /// Used to define if the custom creator interface should be set up to create a new category
    /// or edit an existing one.
    /// </summary>
    public void SetCurrentEditMode(){
        Config.creatingNewCategory = triggersVariant;
    }
}
