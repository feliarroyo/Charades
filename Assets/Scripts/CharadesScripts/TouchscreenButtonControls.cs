using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class implements behaviour for the buttons on-screen.
/// </summary>
public class TouchscreenButtonControls : MonoBehaviour
{
    public bool passType; // True if pressing the button answers the prompt as correct.
    
    /// <summary>
    /// Buttons like these are only shown if the corresponding player preference is active in options.
    /// </summary>
    void Start()
    {
        gameObject.SetActive(PlayerPrefs.GetInt(Const.PREF_SHOWSCREENBUTTONS, 1) == 1);
    }

    /// <summary>
    /// On Android, answer the prompt on touch.
    /// </summary>
    void OnMouseDown(){
        if (Pause.isPaused || !RoundGameplay.isPromptOnScreen)
            return;
        RoundGameplay.round.AnswerPrompt(passType);
    }
}
