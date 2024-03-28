using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class teamSelector : MonoBehaviour
{
    private static string[] labels = new string[2]{"Modo equipos: No", "Modo equipos: Sí"};
    public static int areTeamsEnabled = 0;
    public TextMeshProUGUI text_space;
    public Image button_image;
    public Sprite[] button_sprites;
   
    // Start is called before the first frame update
    void Start()
    {
        text_space.text = labels[areTeamsEnabled];
        this.changeColor();
        button_image.sprite = button_sprites[areTeamsEnabled];
    }

    public void changeColor(){
        if (areTeamsEnabled==1){
            text_space.color = new Color (0.67f,0.286f,0.023f,1f);
        }
        else {
            text_space.color = new Color (0.4f,0.4f,0.4f,1f);
        }
    }
    public void changeTeams(){
        if (areTeamsEnabled == 0) { // enables teams
            areTeamsEnabled++;
            PlayerPrefs.SetInt("teams", 2);
            text_space.text = labels[areTeamsEnabled];
            button_image.sprite = button_sprites[areTeamsEnabled];
        }
        else { // disables teams
            areTeamsEnabled--;
            PlayerPrefs.SetInt("teams", 1);
            text_space.text = labels[areTeamsEnabled];
            button_image.sprite = button_sprites[areTeamsEnabled];
        }
        this.changeColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
