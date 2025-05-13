using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class manages the How To Play related functions.
/// </summary>
public class TutorialSelector : MonoBehaviour
{
    private static string[] labels = new string[11]{
        "JUEGO", 
        "MODOS", 
        "CATEGORÍAS",
        "PRESENTACIÓN",
        "CONTADOR",
        "RONDA",
        "CONTROLES",
        "PUNTUACIÓN",
        "FIN DEL JUEGO",
        "OPCIONES",
        "CATEGORÍAS PERSONALIZADAS"
    };
    public List<GameObject> tutorialPanels = new();
    public int currentLabel = 0;
    public TextMeshProUGUI titleText;
   
    // Start is called before the first frame update
    void Start()
    {
        ShowTutorialText(0);
    }

    /// <summary>
    /// Shows the text for the slide number passed as parameter.
    /// </summary>
    /// <param name="i">Number of slide to show.</param>
    private void ShowTutorialText(int i){
        tutorialPanels[currentLabel].SetActive(false);
        tutorialPanels[i].SetActive(true);
        titleText.text = labels[i];
        currentLabel = i;
    }

    /// <summary>
    /// Goes to the slide following the current one.
    /// </summary>
    public void NextTutorialText(){
        ShowTutorialText((currentLabel+1 < labels.Length)? currentLabel+1 : 0);
    }

    /// <summary>
    /// Goes to the slide previous to the current one.
    /// </summary>
    public void PreviousTutorialText(){
        ShowTutorialText((currentLabel == 0)? labels.Length-1 : currentLabel-1);
    }
}
