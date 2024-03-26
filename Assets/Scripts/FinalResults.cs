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
        //Config.MenuConfig();
        // Solo Mode / Player 1:
        score_1 = CountScore(1);
        
        // Set text (Solo mode)
        if (teamSelector.areTeamsEnabled == 0){
            winner_team_text.text = "¡Fin del juego!";
            score_text.text = "Puntaje\t\t" + score_1;
            return;
        }
        
        // Player 1 (Team Mode behavior)
        string team1 = Competition.teamNames[1];
        string t_score_1 = team1 + "\t\t" + score_1;
        
        // Player 2 (Team Mode only):
        score_2 = CountScore(2);
        string team2 = Competition.teamNames[2];
        string t_score_2 = team2 + "\t\t" + score_2;
        
        // Set text (Team Mode)
        if (score_1 < score_2){
            winner_team_text.text = "¡" + team2 + " gana!";
            score_text.text = "<color=green>" + t_score_2 + "</color>\n" + t_score_1;
        }
        else if (score_1 > score_2){
            winner_team_text.text = "¡" + team1 + " gana!";
            score_text.text = "<color=green>" + t_score_1 + "</color>\n" + t_score_2;
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

    public void Return(){
        Config.MenuConfig();
    }
    public int CountScore(int team){
        int i = 0;
        foreach (int j in Competition.scores[team]){
            i += j;
        }
        return i;
    }
}