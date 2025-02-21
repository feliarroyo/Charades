using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class contains behavior related to the team selector button.
/// </summary>
public class TeamSelector : MonoBehaviour
{
    private static readonly string[] labels = new string[2]{"Modo equipos: No", "Modo equipos: Sí"};
    public TextMeshProUGUI textSpace;
    public Image buttonImage;
    public Sprite[] buttonSprites;

    public GameObject[] teamSpaces;
   
    // Start is called before the first frame update
    void Start()
    {
        ShowTeamsUI(PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1)!=1);
    }

    /// <summary>
    /// Sets button color according to whether teams mode is enabled or not.
    /// </summary>
    public void ChangeTextColor(){
        textSpace.color = (PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1) > 1)? 
            Const.TeamsEnabledTextColor : Const.TeamsDisabledTextColor;
    }

    /// <summary>
    /// Disables teams if they are enabled, and viceversa.
    /// </summary>
    public void ChangeTeams(){
        PlayerPrefs.SetInt(Const.PREF_TEAM_COUNT, (PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1)==1)? 2 : 1);
        ShowTeamsUI(PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1)!=1);
    }

    /// <summary>
    /// Shows or hides Team-related UI.
    /// </summary>
    /// <param name="show">Whether to show or hide UI.</param>
    private void ShowTeamsUI(bool show){
        textSpace.text = labels[show? 0 : 1];
        buttonImage.sprite = buttonSprites[show? 0 : 1];
        foreach (GameObject go in teamSpaces) {
            go.SetActive(show);
        }
        ChangeTextColor();
    }

}
