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
        canDelete = !canDelete;
        foreach (DeleteablePrompt p in promptContainer.GetComponentsInChildren<DeleteablePrompt>(true)){
            p.EnableDeleting(canDelete);
        }
        switch (canDelete) {
            case true:
                buttonText.text = Const.EnglishLocaleActive()? "Stop deleting" : "Dejar de borrar";
                return;
            case false:
                buttonText.text = Const.EnglishLocaleActive()? "Delete prompts" : "Borrar enunciados";
                return;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        canDelete = false;
    }

}
