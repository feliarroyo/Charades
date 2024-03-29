using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class teamSelector : MonoBehaviour
{
    private static string[] labels = new string[2]{"Modo equipos: No", "Modo equipos: Sí"};
    public TextMeshProUGUI text_space;
    public Image button_image;
    public Sprite[] button_sprites;

    public GameObject[] teamSpaces;
   
    // Start is called before the first frame update
    void Start()
    {
        int areTeamsEnabled = PlayerPrefs.GetInt("teams", 1)-1;
        text_space.text = labels[areTeamsEnabled];
        this.changeColor();
        button_image.sprite = button_sprites[areTeamsEnabled];
        if (areTeamsEnabled == 0) {
            foreach (GameObject go in teamSpaces) {
                go.SetActive(false);
            }
        }
    }

    public void changeColor(){
        if (PlayerPrefs.GetInt("teams", 1)>1){
            text_space.color = new Color (0.67f,0.286f,0.023f,1f);
        }
        else {
            text_space.color = new Color (0.4f,0.4f,0.4f,1f);
        }
    }
    public void changeTeams(){
        if (PlayerPrefs.GetInt("teams", 1)==1) { // enables teams
            PlayerPrefs.SetInt("teams", 2);
            text_space.text = labels[1];
            button_image.sprite = button_sprites[1];
            foreach (GameObject go in teamSpaces) {
                go.SetActive(true);
            }
        }
        else { // disables teams
            PlayerPrefs.SetInt("teams", 1);
            text_space.text = labels[0];
            button_image.sprite = button_sprites[0];
            foreach (GameObject go in teamSpaces) {
                go.SetActive(false);
            }
        }
        this.changeColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
