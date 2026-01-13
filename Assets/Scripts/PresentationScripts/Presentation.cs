using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Presentation : MonoBehaviour
{
    public TextMeshProUGUI team_text;
    public TextMeshProUGUI title_text;
    public TextMeshProUGUI desc_text;
    public Category current_category;
    public Image categoryImage;

    // Start is called before the first frame update
    void Start()
    {
        if (Const.EnglishLocaleActive())
        {
            if (PlayerPrefs.GetInt(Const.PREF_TEAM_COUNT, 1)==1)
                team_text.text = Const.NOTEAM_EN + "!";
            else
                team_text.text = "It's " + Competition.GetNextTeamName() + "'s turn!";
        }
        else
            team_text.text = "¡Es el turno de " + Competition.GetNextTeamName() + "!";
        // Mash-Up mode (uses standard presentation when only one category is selected)
            LoadCurrentCategory();
    }

    private void LoadCurrentCategory(){
        current_category = Competition.GetCategory();
        title_text.text = current_category.title;
        desc_text.text = current_category.description;
        categoryImage.sprite = Resources.Load<Sprite>(current_category.iconName);
    }

}
