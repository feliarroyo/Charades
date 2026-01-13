using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    private static DeleteCanvas instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public static void ShowCanvas(bool show) {
        if (instance != null)
        {
            instance.canvas.SetActive(show);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
