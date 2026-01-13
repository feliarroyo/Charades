using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class TeamNameInitializer : MonoBehaviour
{
    [SerializeField] private int teamIndex;

    // Start is called before the first frame update
    void Start()
    {
        string teamName = PlayerPrefs.GetString("Team" + teamIndex, "");
        if (!string.IsNullOrEmpty(teamName))
        {
            GetComponent<TMP_InputField>().text = teamName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
