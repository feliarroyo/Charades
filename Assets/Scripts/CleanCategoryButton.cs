using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanCategoryButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Competition.cleanCatButton = gameObject;
        // Only active when more than one category is selected; unused in Quick Play
        gameObject.SetActive((Competition.categories.Count != 0) && (Competition.gameType != 0));
    }

}