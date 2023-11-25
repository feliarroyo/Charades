using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI score_text, answers_text;
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        string s = "";
        foreach (var a in Score.answers){
            if (a.Value == true)
                s +="<color=\"white\">";
            else
                s +="<color=\"grey\">";
            s += a.Key + "\n";
            // create text, set according to value
        }
        score_text.text = Score.score.ToString();
        answers_text.text = s;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContinueGame(){
        Competition.RegisterScore(Score.score);
    }
}
