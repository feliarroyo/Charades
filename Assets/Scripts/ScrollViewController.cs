using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScrollViewController : MonoBehaviour
{
    public string ScrollTag;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Current ScrollTag: " + PlayerPrefs.GetFloat(ScrollTag, 0));
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, PlayerPrefs.GetFloat(ScrollTag, 656));
    }
    
    public void saveOffset(){
        Debug.Log(gameObject.transform.position.y);
        PlayerPrefs.SetFloat(ScrollTag, gameObject.transform.position.y);
    }
}
