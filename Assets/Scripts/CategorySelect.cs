using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorySelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Competition.SetTeamName(1, "Equipo 1");
        Competition.SetTeamName(2, "Equipo 2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
