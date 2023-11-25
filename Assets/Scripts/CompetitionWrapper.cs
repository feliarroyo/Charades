using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompetitionWrapper : MonoBehaviour
{
    public void AddCategory(Category cat){
        Competition.AddCategory(cat);
    }

    public void StartCompetition(){
        Competition.StartCompetition();
    }

    public void LoadSceneIfCategories(string sceneName){
        if (Competition.HasCategories())
            SceneManager.LoadScene(sceneName);
    }
    

}
