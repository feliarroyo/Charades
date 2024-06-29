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
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, PlayerPrefs.GetFloat(ScrollTag, 0));
    }
    
    public void saveOffset(){
        PlayerPrefs.SetFloat(ScrollTag, gameObject.transform.position.y);
    }
}
