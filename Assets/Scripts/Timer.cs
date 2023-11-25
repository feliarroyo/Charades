using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public int time = 60;
    private static float timeLeft;
    public static bool timerOn;
    public TextMeshProUGUI timerText;
    public bool hasEndSound = false;
    public AudioClip endSound;

    internal static bool TimesUp()
    {
        return (timeLeft == 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
        timeLeft = time;
        timerText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn){
            if (timeLeft > 0) {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else {
                Debug.Log("Time up.");
                if (hasEndSound) GetComponent<AudioSource>().PlayOneShot(endSound);
                timeLeft = 0;
                timerOn = false;
            }
        }
    }

    void UpdateTimer(float currentTime){
        currentTime += 1;
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = seconds.ToString();
    }
}
