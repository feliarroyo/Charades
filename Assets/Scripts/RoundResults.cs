using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundResults : MonoBehaviour
{
    public TextMeshProUGUI score_text, answers_text;
    public MusicPlayer music;
    // Start is called before the first frame update
    void Start()
    {
        music.PlayTrack();
        Config.MenuConfig();
        answers_text.text = FillInAnswers();
        score_text.text = FillInScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContinueGame(){
        Competition.RegisterScore(Score.score);
    }

    private string FillInAnswers(){
        // Returns a list of prompts shown; skipped prompts shown in grey
        string answers = "";
        foreach (var a in Score.answers){
            if (a.Value == true)
                answers +="<color=\"white\">";
            else
                answers +="<color=\"grey\">";
            answers += a.Key + "\n";
        }
        return answers;
    }

    private string FillInScore(){
        // returns score string
        string score = Score.score.ToString() + " acierto";
        if (Score.score != 1)
            score += "s";
        return score;
    }
}
