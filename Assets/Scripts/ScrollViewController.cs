using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollViewController : MonoBehaviour
{
    //public string ScrollTag;

    public ScrollRect scrollRect;
    // Start is called before the first frame update
    
    void OnEnable()
    {
        scrollRect.onValueChanged.AddListener(scrollRectCallBack);
    }

    void scrollRectCallBack(Vector2 value)
    {
        PlayerPrefs.SetFloat("value",value.y);
    }
    void Start()
    {
        float laygiatri = PlayerPrefs.GetFloat("value");
        scrollRect.verticalNormalizedPosition = laygiatri;
    }
    void OnDisable()
    {
        scrollRect.onValueChanged.RemoveListener(scrollRectCallBack);
    }
    
    //void Start()
    //{
        //Debug.Log("Current ScrollTag: " + PlayerPrefs.GetFloat(ScrollTag, 0));
        //gameObject.transform.position = new Vector2(gameObject.transform.position.x, PlayerPrefs.GetFloat(ScrollTag, 656));
    //}
    
    //public void saveOffset(){
      //  Debug.Log(gameObject.transform.position.y);
      //  PlayerPrefs.SetFloat(ScrollTag, gameObject.transform.position.y);
    //}
}