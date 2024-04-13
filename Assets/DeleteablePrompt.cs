using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeleteablePrompt : MonoBehaviour
{
    public TextMeshProUGUI prompt;
    public GameObject cross;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetValue(string value){
        prompt.text = value;
    }
    public void RemovePrompt(){
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "CustomCreator")
            GameObject.Find("CategoryCreator").GetComponent<CategoryCreator>().RemoveQuestion(prompt.text);
        else
            GameObject.Find("CategoryEditor").GetComponent<CategoryEditor>().RemoveQuestion(prompt.text);
        Destroy(gameObject);
    }

    public void EnableDeleting(bool canDelete){
        cross.SetActive(canDelete);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
