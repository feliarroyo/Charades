using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RoundResults : MonoBehaviour
{
    public TextMeshProUGUI score_text;
    public TextMeshProUGUI answers_text;
    public TextMeshProUGUI skips_text;
    public MusicPlayer music;

    // Start is called before the first frame update
    void Start()
    {
        music.PlayTrack();
        Config.MenuConfig();
        answers_text.text = FillInAnswers();
        score_text.text = FillInScore();
        int skips = Score.GetSkipCount();
        skips_text.text = skips == 0? "" : "(" + skips + " sin responder)";
    }

    public void ContinueGame(){
        Competition.RegisterScore(Score.score);
    }

    private string FillInAnswers(){
        // Returns a list of prompts shown; skipped prompts shown in grey
        string answers = "";
        foreach (var a in Score.answers){
            // Format according to if it was answered correctly or not
            if (a.Item2 == true)
                answers +="<color=\"white\">";
            else
                answers +="<color=\"grey\">";
            // Truncate longer prompts
            if (a.Item1.Length > 40)
                answers += a.Item1.Truncate(40) + "\n";
            else
                answers += a.Item1 + "\n";
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
