using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalResults : MonoBehaviour
{
    public TextMeshProUGUI winner_team_text, score_text;
    private List<int> scores;
    private bool tiedGame;
    private int winnerIndex, winnerScore;

    // Start is called before the first frame update
    void Start()
    {
        CountAllScores();
        WriteScreen();       
    }

    private void WriteScreen(){
        if (PlayerPrefs.GetInt("teams", 1) == 1) // Single-player game
            WriteText("¡Fin del juego!", $"Puntaje\t\t{scores[0]}");
        else {
            if (tiedGame)
                WriteText("¡Empate!", $"{GetTeamScoreText(0)}\n{GetTeamScoreText(1)}");
            else
                WriteText($"¡{Competition.teamNames[winnerIndex]} gana!", GetAllTeamScoreText());
        }
    }

    // currently hard-coded for two teams, change for potentially larger numbers of teams.
    private string GetAllTeamScoreText(){
        if (scores[0] < scores[1])
            return $"{GetTeamScoreText(1)}\n{GetTeamScoreText(0)}";
        else
            return $"{GetTeamScoreText(0)}\n{GetTeamScoreText(1)}";
    }

    // Returns score text formatted to be written per team.
    private string GetTeamScoreText(int i){
        bool isWinner = scores[i]==winnerScore;
        return $"{Competition.teamNames[i]}\t\t{(isWinner? $"<color=green>{scores[i]}</color>" : $"{scores[i]}")}";
    }
    
    // Writes winner and score texts.
    private void WriteText(string winner, string score){
        winner_team_text.text = winner;
        score_text.text = score;
    }

    // Gets every team's scores.
    private void CountAllScores(){
        scores = new List<int>();
        winnerIndex = -1;
        winnerScore = -1;
        tiedGame = false;
        for (int i = 0; i < PlayerPrefs.GetInt("teams", 1); i++){
            int newScore = CountScore(i);
            scores.Add(newScore);
            if (newScore > winnerScore){
                winnerScore = newScore;
                winnerIndex = i;
                tiedGame = false;
            }
            else if (newScore == winnerScore){
                tiedGame = true;
            }
        }
    }

    // Gets total score of a given team.
    private int CountScore(int team){
        int i = 0;
        foreach (int j in Competition.scores[team]){
            i += j;
        }
        return i;
    }
}