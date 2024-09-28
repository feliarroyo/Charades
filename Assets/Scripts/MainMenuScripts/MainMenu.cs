using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI versionText;
    // Start is called before the first frame update
    void Start()
    {
        versionText.text = 'v' + Application.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
