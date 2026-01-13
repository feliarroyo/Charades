using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class keeps track of the score during a round, as well as the prompts shown and
/// whether they have been answered or not.
/// </summary>
public class Score : MonoBehaviour
{
    public static List<(string, bool)> answers = new();
    public static int score = 0;

    /// <summary>
    /// Clears the score, restarting to 0 points and erasing all previous answers saved.
    /// </summary>
    public static void ClearScore(){
        answers.Clear();
        score = 0;
    }

    /// <returns>Text string according to the score currently obtained.</returns>
    public static string GetScoreText(){
        if (Const.EnglishLocaleActive())
        {
            return (score == 1) ? "1 point" : score + " points";
        }
        else
        {
            return (score == 1) ? "1 acierto" : score + " aciertos";
        }
    }

    public static int GetSkipCount(){
        return answers.Count(ans => ans.Item2 == false);
    }
}