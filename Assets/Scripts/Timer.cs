using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public int time = 60;
    private float timeLeft;
    public bool timerOn;
    public TextMeshProUGUI timerText;
    public bool hasEndSound = false, hasTickSound = false;
    public AudioClip tickSound, endSound;
    private AudioSource soundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
        timeLeft = time;
        soundPlayer = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
        timerText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        soundPlayer.PlayOneShot(tickSound);
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
                if (hasEndSound) soundPlayer.PlayOneShot(endSound);
                Debug.Log("El tiempo se encuentra apagado");
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
        if (hasTickSound && (seconds.ToString() != timerText.text) && (seconds.ToString() != "0")) soundPlayer.PlayOneShot(tickSound);
        timerText.text = seconds.ToString();
    }
}
