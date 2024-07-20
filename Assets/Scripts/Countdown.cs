using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    private Timer timer;
    public GameObject button1, button2;
    // Start is called before the first frame update
    void Start()
    {
        Config.GameplayConfig();
        timer = GameObject.Find("Time").GetComponent<Timer>();
        if (PlayerPrefs.GetInt("showScreenButtons", 1) == 1){
            button1.SetActive(true);
            button2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            Screen.orientation = ScreenOrientation.LandscapeRight;
        if (timer.TimeIsUp())
            SceneManager.LoadScene("Round");
    }
}