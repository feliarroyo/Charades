using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalResults : MonoBehaviour
{
    public TextMeshProUGUI winner_team_text;
    public TextMeshProUGUI name_text;
    public TextMeshProUGUI score_text;
    public TextMeshProUGUI changeCatText;
    private List<int> scores;
    private bool tiedGame;
    private int winnerIndex, winnerScore;
    private const string changeCategoryName = "Cambiar categorías";
    private const string changeCategoryName_EN = "Change Categories";
    private const string changeCategoryName_EN_singular = "Change Category";

    // Start is called before the first frame update
    void Start()
    {
        CountAllScores();
        WriteScreen();    
    }

    private void WriteScreen(){
        if (PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1) == 1) { // Single-player game
            if (Const.EnglishLocaleActive())
                WriteText("Game Over!", "Score", $"{scores[0]}");
            else
                WriteText("¡Fin del juego!", "Puntaje", $"{scores[0]}");
        }
        else
        {
            if (tiedGame)
                WriteText(Const.EnglishLocaleActive() ? "It's a tie!" : "¡Empate!", $"{GetAllTeamNameText()}", $"{GetAllTeamScoreText()}");
            else
                WriteText(GetWinnerTeamHeader(), $"{GetAllTeamNameText()}", $"{GetAllTeamScoreText()}");
        }
        // Change button text on QP
        changeCatText.text = Const.EnglishLocaleActive() ? changeCategoryName_EN : changeCategoryName;
        if (Competition.gameType == 0)
        {
            changeCatText.text = Const.EnglishLocaleActive() ? changeCategoryName_EN_singular : changeCategoryName.TrimEnd('s');
        }
    }

    private string GetWinnerTeamHeader()
    {
        if (Const.EnglishLocaleActive())
            return $"{Competition.teamNames[winnerIndex]} Wins!";
        else
            return $"¡{Competition.teamNames[winnerIndex]} gana!";
    }
    // currently hard-coded for two teams, change for potentially larger numbers of teams.
    private string GetAllTeamScoreText()
    {
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
        return $"{(isWinner? $"<color=green>{Competition.teamNames[i]}</color>" : $"{Competition.teamNames[i]}")}";

        
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
        for (int i = 0; i < PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1); i++){
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