using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class RoundGameplay : MonoBehaviour
{
    protected static Color DefaultColor = new(0.1921569f,0.3019608f,0.4745098f,1f);
    private int score;
    public static Camera cam;
    public static List<string> prompts;
    protected static bool isActive;
    private TextMeshProUGUI prompt_text, hits_text;
    public AudioClip correctSound, skipSound, finishSound;
    public static Category current_category;
    private Timer timer;
    private bool isGameOver;

    // Mash-Up only
    public static Dictionary<string, List<string> > mashupPrompts;
    public static Category currentMashUpCategory;

    // constants
    private const string 
        CORRECT = "Bien", 
        SKIP = "Paso", 
        TIMEUP = "¡Tiempo!",
        SINGLE_HITCOUNT = "1 acierto", 
        MULTIPLE_HITCOUNT = " aciertos";

    // control values
    private const float VALUE_GETANSWER = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        Config.GameplayConfig();
        Pause.isPaused = false;
        isGameOver = false;
        Score.answers.Clear();
        isActive = true;
        cam = FindObjectOfType<Camera>().GetComponent<Camera>();
        prompt_text = GameObject.Find("Prompt").GetComponent<TextMeshProUGUI>();
        hits_text = GameObject.Find("Points").GetComponent<TextMeshProUGUI>();
        score = 0;
        InitializePrompts();
        timer = GameObject.Find("Time").GetComponent<Timer>();
        timer.SetTime(PlayerPrefs.GetInt("roundDuration", 60));
    }

    private void InitializePrompts(){
        switch (Competition.game_mode){
            case GameConstants.GameModes.MashUp:
                mashupPrompts = Competition.GetMashUpPrompts();
                break;
            default:
                current_category = Competition.GetCategory();
                prompts = Competition.GetPrompts(current_category);
                break;
        }
        GetNewPrompt();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.isPaused)
            return;
        else if (timer.TimeIsUp())
            StartCoroutine(EndGame());
        
        // Each frame, we check the tilting of the phone. If tilted enough up, it gives the point. In the opposite case, it does not.
        Vector3 inclinacion = new(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
        //gyro_test.text = inclinacion.ToString();
        if ((Application.platform == RuntimePlatform.WindowsPlayer) || (Application.platform == RuntimePlatform.WindowsEditor)){
            if (Input.GetButton("Pass"))
                NextQuestion(true);
            else if (Input.GetButton("Fail"))
                NextQuestion(false);
        }
        if (PlayerPrefs.GetInt("useMotionControls", 1) != 1)
            return;
        if ((inclinacion.x > 0.3f) || (inclinacion.x < -0.3f) || (inclinacion.z > 0.9f) || (inclinacion.z < -0.9f))
            return;
        else if (inclinacion.z > VALUE_GETANSWER)
            NextQuestion(true);
        else if (inclinacion.z < -VALUE_GETANSWER)
            NextQuestion(false);
    }

    public void NextQuestion(bool givePoint){
        // Changes the text of the prompt and gives a point on TRUE.
        if (!isActive | Pause.isPaused | isGameOver)
            return;
        if (givePoint)
            UpdateScore();
        isActive = false;
        StartCoroutine(ChangeScreen(givePoint));
    }

    private void ChangeScreen(Color color, string text, AudioClip clip){
        cam.backgroundColor = color;
        prompt_text.text = text;
        try {
            SFXManager.instance.PlayClip(clip);
        }
        catch (NullReferenceException){

        }
    }

    private IEnumerator ChangeScreen(bool givePoint = false)
    // remove current word from the pool, and return a new word within the pile
    {
        RemovePromptFromPool();
        Score.answers.Add((prompt_text.text, givePoint));
        if (givePoint)
            ChangeScreen(Color.green, CORRECT, correctSound);
        else
            ChangeScreen(Color.red, SKIP, skipSound);
        float answerWait = PlayerPrefs.GetFloat(GameConstants.PREF_WAITDURATION, 1f);
        yield return new WaitForSeconds(0.5f);
        hits_text.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(answerWait - 0.5f);
        this.GetNewPrompt();
    }

    private void RemovePromptFromPool(){
        switch (Competition.game_mode){
            case GameConstants.GameModes.MashUp:
                mashupPrompts[currentMashUpCategory.category].Remove(prompt_text.text);
                break;
            default:
                prompts.Remove(prompt_text.text);
                break;
        }
    }

    private void GetNewPrompt(){
        switch (Competition.game_mode){
            case GameConstants.GameModes.MashUp:
                currentMashUpCategory = Competition.GetRandomMashUpCategory();
                string nextCat = currentMashUpCategory.category;
                if (mashupPrompts[nextCat].Count == 0){ // if no more questions, restart prompt pool
                    mashupPrompts[nextCat] = new(currentMashUpCategory.questions);
                }   
                if (!isGameOver){
                    isActive = true;
                    cam.backgroundColor = DefaultColor;
                    prompt_text.text = mashupPrompts[nextCat][UnityEngine.Random.Range(0, mashupPrompts[nextCat].Count)];
                }
                break;
            default:
                if (prompts.Count == 0) // if no more questions, restart prompt pool
                    prompts = new(current_category.questions);
                if (!isGameOver){
                    isActive = true;
                    cam.backgroundColor = DefaultColor;
                    prompt_text.text = prompts[UnityEngine.Random.Range(0, prompts.Count)];
                }
                break;
        }
    }

    private void UpdateScore()
    // remove current word from the pool, and return a new word within the pile
    {
        score++;
        hits_text.color = new Color(255, 187, 0, 255);
        if (score == 1) 
            hits_text.text = SINGLE_HITCOUNT;
        else 
            hits_text.text = score + MULTIPLE_HITCOUNT;
    }

    public IEnumerator EndGame(){
        isGameOver = true;
        if (PlayerPrefs.GetInt(GameConstants.PREF_SHOWSCREENBUTTONS, 1) == 1) {
            GameObject.Find("Pause Button").SetActive(false);
            GameObject.Find("Hit Button").SetActive(false);
            GameObject.Find("Pass Button").SetActive(false);
        }
        Pause.isPaused = true;
        if (isActive){
            Score.answers.Add((prompt_text.text, false));
            RemovePromptFromPool();
        }
        ChangeScreen(Color.magenta, TIMEUP, finishSound);
        Score.score = score;
        RecordUsedPrompts();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("RoundResults");
    }

    private void RecordUsedPrompts(){
        switch (Competition.game_mode){
            case GameConstants.GameModes.MashUp:
                foreach (string c in mashupPrompts.Keys){
                    Competition.session_categories[c] = mashupPrompts[c];
                }
                break;
            default:
                Competition.session_categories[current_category.category] = prompts;
                break;
        }
    }
}