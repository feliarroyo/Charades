using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Presentation : MonoBehaviour
{
    public TextMeshProUGUI team_text, title_text, desc_text;
    public Category current_category;

    // Start is called before the first frame update
    void Start()
    {
        current_category = Competition.GetCategory();
        team_text.text = "¡Es el turno de " + Competition.GetNextTeamName() + "!";
        title_text.text = current_category.category;
        desc_text.text = current_category.description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
