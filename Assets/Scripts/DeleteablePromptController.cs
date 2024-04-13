using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeleteablePromptController : MonoBehaviour
{
    public GameObject promptContainer;
    public static bool canDelete;
    public TextMeshProUGUI buttonText;
    public void SetDeleteablePrompts() {
        foreach (DeleteablePrompt p in promptContainer.GetComponentsInChildren<DeleteablePrompt>(true)){
            p.EnableDeleting(canDelete);
        }
        canDelete = !canDelete;
        switch (canDelete) {
            case true:
                buttonText.text = "Borrar enunciados";
                return;
            case false:
                buttonText.text = "Dejar de borrar";
                return;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        canDelete = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
