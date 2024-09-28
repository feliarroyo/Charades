using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeamSelector : MonoBehaviour
{
    private static string[] labels = new string[2]{"Modo equipos: No", "Modo equipos: Sí"};
    public TextMeshProUGUI text_space;
    public Image button_image;
    public Sprite[] button_sprites;

    public GameObject[] teamSpaces;
   
    // Start is called before the first frame update
    void Start()
    {
        int areTeamsEnabled = PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1)-1;
        text_space.text = labels[areTeamsEnabled];
        ChangeColor();
        button_image.sprite = button_sprites[areTeamsEnabled];
        if (areTeamsEnabled == 0) {
            foreach (GameObject go in teamSpaces) {
                go.SetActive(false);
            }
        }
    }

    public void ChangeColor(){
        if (PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1)>1){
            text_space.color = new Color (0.67f,0.286f,0.023f,1f);
        }
        else {
            text_space.color = new Color (0.4f,0.4f,0.4f,1f);
        }
    }
    public void ChangeTeams(){
        if (PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1)==1) { // enables teams
            PlayerPrefs.SetInt(Const.PREF_TEAM_COUNT, 2);
            text_space.text = labels[1];
            button_image.sprite = button_sprites[1];
            foreach (GameObject go in teamSpaces) {
                go.SetActive(true);
            }
        }
        else { // disables teams
            PlayerPrefs.SetInt(Const.PREF_TEAM_COUNT, 1);
            text_space.text = labels[0];
            button_image.sprite = button_sprites[0];
            foreach (GameObject go in teamSpaces) {
                go.SetActive(false);
            }
        }
        ChangeColor();
    }

}
