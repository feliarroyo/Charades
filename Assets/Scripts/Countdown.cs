using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        Config.GameplayConfig();
        timer = GameObject.Find("Time").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.TimeIsUp())
            SceneManager.LoadScene("Round");
    }
}