using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeleteablePrompt : MonoBehaviour
{
    public TextMeshProUGUI prompt;
    public GameObject cross;

    public void SetValue(string value){
        prompt.text = value;
    }
    public void RemovePrompt(){
        string currentScene = SceneManager.GetActiveScene().name;
        GameObject.Find("CategoryCreator").GetComponent<CategoryCreator>().RemoveQuestion(prompt.text);
        Destroy(gameObject);
    }

    public void EnableDeleting(bool canDelete){
        cross.SetActive(canDelete);
    }
}
