using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalResults : MonoBehaviour
{
    public TextMeshProUGUI winner_team_text, name_text, score_text, changeCatText;
    private List<int> scores;
    private bool tiedGame;
    private int winnerIndex, winnerScore;
    private const string 
        CHANGE_CATEGORY = "Cambiar categorías", 
        GAME_OVER = "¡Fin del juego!", 
        SCORE_LABEL = "Puntaje", 
        TIE = "¡Empate!";

    // Start is called before the first frame update
    void Start()
    {
        CountAllScores();
        WriteScreen();    
    }

    private void WriteScreen(){
        if (PlayerPrefs.GetInt(GameConstants.PREF_TEAM_COUNT, 1) == 1) // Single-player game
            WriteText(GAME_OVER, SCORE_LABEL, $"{scores[0]}");
        else {
            if (tiedGame)
                WriteText(TIE, $"{GetAllTeamNameText()}", $"{GetAllTeamScoreText()}");
            else
                WriteText($"¡{Competition.team_names[winnerIndex]} gana!", $"{GetAllTeamNameText()}",$"{GetAllTeamScoreText()}");
        }
        changeCatText.text = CHANGE_CATEGORY;
        // Change button text on QP
        if (Competition.game_mode == GameConstants.GameModes.QuickPlay)
            changeCatText.text = CHANGE_CATEGORY.TrimEnd('s');
    }

    // currently hard-coded for two teams, change for potentially larger numbers of teams.
    private string GetAllTeamScoreText(){
        if (scores[0] < scores[1])
            return $"{GetTeamScoreText(1)}\n{GetTeamScoreText(0)}";
        else
            return $"{GetTeamScoreText(0)}\n{GetTeamScoreText(1)}";
    }

    private string GetAllTeamNameText(){
        if (scores[0] < scores[1])
            return $"{GetTeamNameText(1)}\n{GetTeamNameText(0)}";
        else
            return $"{GetTeamNameText(0)}\n{GetTeamNameText(1)}";
    }

    // Returns score text formatted to be written per team.
    private string GetTeamScoreText(int i){
        bool isWinner = scores[i]==winnerScore;
        return $"{(isWinner? $"<color=green>{scores[i]}</color>" : $"{scores[i]}")}";
    }

    private string GetTeamNameText(int i){
        bool isWinner = scores[i]==winnerScore;
        return $"{(isWinner? $"<color=green>{Competition.team_names[i]}</color>" : $"{Competition.team_names[i]}")}";
    }
    
    // Writes winner and score texts.
    private void WriteText(string winner, string name, string score){
        winner_team_text.text = winner;
        score_text.text = score;
        name_text.text = name;
    }

    // Gets every team's scores.
    private void CountAllScores(){
        scores = new List<int>();
        winnerIndex = -1;
        winnerScore = -1;
        tiedGame = false;
        for (int i = 0; i < PlayerPrefs.GetInt(GameConstants.PREF_TEAM_COUNT, 1); i++){
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
        try {
            foreach (int j in Competition.scores[team]){
                i += j;
            }
        }
        catch (KeyNotFoundException) {
            Debug.Log("Warning: No scores were registered.");
        }
        
        return i;
    }
}