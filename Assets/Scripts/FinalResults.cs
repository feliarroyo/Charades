using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalResults : MonoBehaviour
{
    public TextMeshProUGUI winner_team_text, score_text;
    public MusicPlayer music;
    private int score_1, score_2;
    // Start is called before the first frame update
    void Start()
    {
        music.PlayTrack();
        Config.MenuConfig();
        string team1 = Competition.teamNames[1];
        string team2 = Competition.teamNames[2];
        score_1 = CountScore(1);
        string t_score_1 = team1 + "\t" + score_1;
        score_2 = CountScore(2);
        string t_score_2 = team2 + "\t" + score_2;
        if (score_1 < score_2){
            winner_team_text.text = "¡" + team2 + " gana!";
            score_text.text = t_score_2 + "\n" + t_score_1;
        }
        else if (score_1 > score_2){
            winner_team_text.text = "¡" + team1 + " gana!";
            score_text.text = t_score_1 + "\n" + t_score_2;
        }
        else {
            winner_team_text.text = "¡Empate!";
            score_text.text = t_score_1 + "\n" + t_score_2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CountScore(int team){
        int i = 0;
        foreach (int j in Competition.scores[team]){
            i += j;
        }
        return i;
    }
}