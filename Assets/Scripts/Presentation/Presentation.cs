using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Presentation : MonoBehaviour
{
    public TextMeshProUGUI team_text, title_text, desc_text;
    private Category current_category;
    public Image categoryImage;

    // Start is called before the first frame update
    void Start()
    {
        team_text.text = "¡Es el turno de " + Competition.GetNextTeamName() + "!";
        LoadCurrentCategory();
    }

    private void LoadCurrentCategory(){
        current_category = Competition.GetCategory();
        title_text.text = current_category.category;
        desc_text.text = current_category.description;
        categoryImage.sprite = Resources.Load<Sprite>(current_category.iconName);
    }

}
