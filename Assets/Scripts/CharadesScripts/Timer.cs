using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public int time = 60;
    public int critical_time = 5; // when should beep sounds be played
    private float time_left;
    public bool timer_on;
    private TextMeshProUGUI timer_text;
    public bool hasEndSound = false, hasTickSound = false;
    public AudioClip tickSound, endSound;
    private const float TIME_OVER = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timer_on = true;
        time_left = time;
        timer_text = GetComponent<TextMeshProUGUI>();
        UpdateTimer(time_left);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer_on){
            if (time_left > 0) {
                time_left -= Time.deltaTime;
                UpdateTimer(time_left);
            }
            else {
                timer_text.color = Color.white;
                try {
                    if (hasEndSound) {
                        SFXManager.instance.PlayClip(endSound);
                    }
                }
                catch (NullReferenceException){
                    Debug.Log("Warning: No sound detected");
                }
                time_left = 0;
                timer_on = false;
            }
        }
    }

    internal bool TimeIsUp()
    {
        return time_left == TIME_OVER;
    }

    void UpdateTimer(float currentTime){
        currentTime += 1;
        float seconds = Mathf.FloorToInt(currentTime % (time + 1));
        try {
            if (hasTickSound && (seconds.ToString() != timer_text.text) && (seconds.ToString() != "0") && (seconds <= critical_time)) 
                SFXManager.instance.PlayClip(tickSound);
        }
        catch (NullReferenceException){
            Debug.Log("No sound manager detected");
        }
        timer_text.text = seconds.ToString();
        if (seconds == critical_time) {
            timer_text.color = new Color(1f, 0.39f, 0f, 1f);
        }
    }

    public void SetTime(int newTime){
        time = newTime;
        time_left = newTime;
    }
}
