using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Presentation : MonoBehaviour
{
    public TextMeshProUGUI team_text, title_text, desc_text;
    public Category current_category;
    public Image categoryImage;

    // Start is called before the first frame update
    void Start()
    {
        team_text.text = "¡Es el turno de " + Competition.GetNextTeamName() + "!";
        switch (Competition.gameType){
            case 2: // Mash-Up mode
                title_text.text = "Mash-Up";
                desc_text.text = "¡Pueden tocar enunciados de cualquiera de las categorías seleccionadas!";
                categoryImage.sprite = Resources.Load<Sprite>("remix");
                break;
            default: // Other modes
                current_category = Competition.GetCategory();
                title_text.text = current_category.category;
                desc_text.text = current_category.description;
                categoryImage.sprite = Resources.Load<Sprite>(current_category.iconName);
                break;
        }
    }

}
