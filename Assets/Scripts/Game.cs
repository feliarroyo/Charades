using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static Color DefaultColor = new(0.1921569f,0.3019608f,0.4745098f,1f);
    public int score;
    public static Camera cam;
    public static List<string> prompts;
    
    private static bool isActive;

    public TextMeshProUGUI prompt_text, hits_text, gyro_test;

    public AudioClip pass_sound, fail_sound, end_sound;

    public static Category cat;


    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Pause.isPaused = false;
        Score.answers.Clear();
        prompts = new();
        isActive = true;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        cam = FindObjectOfType<Camera>().GetComponent<Camera>();
        prompt_text = GameObject.Find("Prompt").GetComponent<TextMeshProUGUI>();
        hits_text = GameObject.Find("Points").GetComponent<TextMeshProUGUI>();
        score = 0;
        cat = Competition.GetCategory();
        prompts = cat.questions;
        GetNewPrompt();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.isPaused)
            return;
        else if (Timer.TimesUp())
            StartCoroutine(EndGame());
        // Each frame, we check the tilting of the phone. If tilted enough up, it gives the point. In the opposite case, it does not.
        Vector3 inclinacion = new(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
        gyro_test.text = inclinacion.ToString();
        
        
        if ((inclinacion.x > 0.3f) || (inclinacion.x < -0.3f) || (inclinacion.z > 0.9f) || (inclinacion.z < -0.9f))
            return;
        else if ((inclinacion.z > 0.7f) || (Input.GetKey(KeyCode.E)))
            Pass(true);
        else if ((inclinacion.z < -0.7f) || (Input.GetKey(KeyCode.Q)))
            Pass(false);
    }

    // pass changes the text of the prompt, as well as giving a point if givePoint is set on 'true'.
    public void Pass(bool givePoint){
        if (!isActive)
            return;
        if (givePoint)
            this.UpdateScore();
        isActive = false;
        StartCoroutine(ChangeScreen(givePoint));;
    }

    private IEnumerator ChangeScreen(bool givePoint = false)
    // remove current word from the pool, and return a new word within the pile
    {
        
        prompts.Remove(prompt_text.text);
        Score.answers.Add(prompt_text.text, givePoint);
        if (givePoint){
            cam.backgroundColor = Color.green;
            prompt_text.text = "Bien";
            GetComponent<AudioSource>().PlayOneShot(pass_sound);
        }
        else{
            cam.backgroundColor = Color.red;
            prompt_text.text = "Paso";
            GetComponent<AudioSource>().PlayOneShot(fail_sound);
        }
        yield return new WaitForSeconds(0.9f);
        this.GetNewPrompt();
    }

    private void GetNewPrompt(){
        isActive = true;
        cam.backgroundColor = DefaultColor;
        prompt_text.text = prompts[Random.Range(0, prompts.Count)];
    }

    private void UpdateScore()
    // remove current word from the pool, and return a new word within the pile
    {
        score++;
        if (score == 1) 
            hits_text.text = "1 acierto";
        else 
            hits_text.text = score + " aciertos";
    }

    public IEnumerator EndGame(){
        Pause.isPaused = true;
        cam.backgroundColor = Color.magenta;
        prompt_text.text = "¡Tiempo!";
        GetComponent<AudioSource>().PlayOneShot(end_sound);
        Score.score = score;
        yield return new WaitForSeconds(3f);
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        SceneManager.LoadScene("GameEnd");
    }
}