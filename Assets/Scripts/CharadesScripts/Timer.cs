using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public int time = 60;
    public int criticalTime = 5; // when should beep sounds be played
    private float timeLeft;
    public bool timerOn;
    public TextMeshProUGUI timerText;
    public bool hasEndSound = false;
    public bool hasTickSound = false;
    public AudioClip tickSound, endSound;
    private AudioSource soundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
        timeLeft = time;
        soundPlayer = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
        timerText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        UpdateTimer(timeLeft);
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
                timerText.color = new Color(255, 255, 255, 255);
                if (hasEndSound) {
                    soundPlayer.PlayOneShot(endSound);
                }
                timeLeft = 0;
                timerOn = false;
            }
        }
    }

    internal bool TimeIsUp()
    {
        return timeLeft == 0.0f;
    }

    void UpdateTimer(float currentTime){
        currentTime += 1;
        float seconds = Mathf.FloorToInt(currentTime % (time + 1));
        if (hasTickSound && (seconds.ToString() != timerText.text) && (seconds.ToString() != "0") && (seconds <= criticalTime)) 
            soundPlayer.PlayOneShot(tickSound);
        timerText.text = seconds.ToString();
        if (seconds == 5) {
            timerText.color = new Color(1f, 0.39f, 0f, 1f);
        }
    }

    public void SetTime(int newTime){
        time = newTime;
        timeLeft = newTime;
    }
}
