using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanCategoryButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Competition.cleanCatButton = gameObject;
        gameObject.SetActive(Competition.categories.Count != 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
