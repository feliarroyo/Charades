using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the How To Play related functions.
/// </summary>
public class TutorialSelector : MonoBehaviour
{
    private int count;
    public List<GameObject> tutorialPanels = new();
    private int currentLabel = 0;

    // Start is called before the first frame update
    void Start()
    {
        ShowTutorialText(0);
        count = tutorialPanels.Count;
    }

    /// <summary>
    /// Shows the text for the slide number passed as parameter.
    /// </summary>
    /// <param name="i">Number of slide to show.</param>
    private void ShowTutorialText(int i){
        tutorialPanels[currentLabel].SetActive(false);
        tutorialPanels[i].SetActive(true);
        currentLabel = i;
    }

    public void ResetTutorialText(){
        ShowTutorialText(0);
    }

    /// <summary>
    /// Goes to the slide following the current one.
    /// </summary>
    public void NextTutorialText()
    {
        ShowTutorialText((currentLabel + 1 < count) ? currentLabel + 1 : 0);
    }

    /// <summary>
    /// Goes to the slide previous to the current one.
    /// </summary>
    public void PreviousTutorialText(){
        ShowTutorialText((currentLabel == 0)? count-1 : currentLabel-1);
    }
}
