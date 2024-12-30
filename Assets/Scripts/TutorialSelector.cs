using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public List<GameObject> tutorialPanels = new List<GameObject>();
    public int currentLabel = 0;
    public TextMeshProUGUI text_space;
   
    // Start is called before the first frame update
    void Start()
    {
        ShowTutorialText(0);
    }

    private void ShowTutorialText(int i){
        tutorialPanels[currentLabel].SetActive(false);
        tutorialPanels[i].SetActive(true);
        text_space.text = labels[i];
        currentLabel = i;
    }

    public void NextTutorialText(){
        if (currentLabel+1 < labels.Length)
            ShowTutorialText(currentLabel+1);
        else
            ShowTutorialText(0);
    }

    public void PreviousTutorialText(){
        if (currentLabel == 0)
            ShowTutorialText(labels.Length-1);
        else
            ShowTutorialText(currentLabel-1);
    }
}
