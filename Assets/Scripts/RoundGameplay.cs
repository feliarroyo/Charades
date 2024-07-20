using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RoundGameplay : MonoBehaviour
{
    protected static Color DefaultColor = new(0.1921569f,0.3019608f,0.4745098f,1f);
    public int score;
    public static Camera cam;
    public static List<string> prompts;
    protected static bool isActive;
    public TextMeshProUGUI prompt_text, hits_text; //, gyro_test;
    public SoundEffectPlayer[] sounds; // 0 = correct, 1 = pass prompt, 2 = finish
    public static Category current_category;
    public Timer timer;
    public bool isGameOver;

    // Mash-Up only
    public static Dictionary<string, List<string> > mashupPrompts;
    public static Category currentMashUpCategory;

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
        Debug.Log("New Duration: " + PlayerPrefs.GetInt("roundDuration"));
        timer.SetTime(PlayerPrefs.GetInt("roundDuration", 60));
    }

    private void InitializePrompts(){
        switch (Competition.gameType){
            case 2:
                mashupPrompts = Competition.GetMashUpPrompts();
                GetNewPrompt();
                break;
            default:
                current_category = Competition.GetCategory();
                prompts = Competition.GetPrompts(current_category);
                Debug.Log("Current session has this # of prompts: " + prompts.Count);
                GetNewPrompt();
                break;
        }
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
                Pass(true);
            else if (Input.GetButton("Fail"))
                Pass(false);
        }
        if (PlayerPrefs.GetInt("useMotionControls", 1) != 1)
            return;
        if ((inclinacion.x > 0.3f) || (inclinacion.x < -0.3f) || (inclinacion.z > 0.9f) || (inclinacion.z < -0.9f))
            return;
        else if (inclinacion.z > 0.7f)
            Pass(true);
        else if (inclinacion.z < -0.7f)
            Pass(false);
    }

    public void Pass(bool givePoint){
        // Changes the text of the prompt and gives a point on TRUE.
        if (!isActive | Pause.isPaused | isGameOver)
            return;
        if (givePoint)
            UpdateScore();
        isActive = false;
        StartCoroutine(ChangeScreen(givePoint));
    }

    private IEnumerator ChangeScreen(bool givePoint = false)
    // remove current word from the pool, and return a new word within the pile
    {
        RemovePromptFromPool();
        Score.answers.Add((prompt_text.text, givePoint));
        if (givePoint){
            cam.backgroundColor = Color.green;
            prompt_text.text = "Bien";
            sounds[0].PlayClip();
        }
        else{
            cam.backgroundColor = Color.red;
            prompt_text.text = "Paso";
            sounds[1].PlayClip();
        }
        float answerWait = PlayerPrefs.GetFloat("answerWaitDuration", 1f);
        yield return new WaitForSeconds(0.5f);
        hits_text.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(answerWait - 0.5f);
        this.GetNewPrompt();
    }

    private void RemovePromptFromPool(){
        switch (Competition.gameType){
            case 2:
                mashupPrompts[currentMashUpCategory.category].Remove(prompt_text.text);
                break;
            default:
                prompts.Remove(prompt_text.text);
                break;
        }
    }

    private void GetNewPrompt(){
        switch (Competition.gameType){
            case 2:
                currentMashUpCategory = Competition.GetRandomMashUpCategory();
                string nextCat = currentMashUpCategory.category;
                if (mashupPrompts[nextCat].Count == 0){ // if no more questions, restart prompt pool
                    mashupPrompts[nextCat] = new(currentMashUpCategory.questions);
                    Debug.Log("Come in!");
                }   
                if (!isGameOver){
                    isActive = true;
                    cam.backgroundColor = DefaultColor;
                    Debug.Log("After Prompt Count: " + mashupPrompts[nextCat].Count);
                    prompt_text.text = mashupPrompts[nextCat][Random.Range(0, mashupPrompts[nextCat].Count)];
                }
                break;
            default:
                if (prompts.Count == 0){ // if no more questions, restart prompt pool
                    prompts = new(current_category.questions);
                    Debug.Log("Session restarted has this # of prompts: " + prompts.Count);
                }   
                if (!isGameOver){
                    isActive = true;
                    cam.backgroundColor = DefaultColor;
                    Debug.Log("It's not over! # of prompts: " + prompts.Count);
                    prompt_text.text = prompts[Random.Range(0, prompts.Count)];
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
            hits_text.text = "1 acierto";
        else 
            hits_text.text = score + " aciertos";
    }

    public IEnumerator EndGame(){
        isGameOver = true;
        if (PlayerPrefs.GetInt("showScreenButtons", 1) == 1) {
            GameObject.Find("Pause Button").SetActive(false);
            GameObject.Find("Hit Button").SetActive(false);
            GameObject.Find("Pass Button").SetActive(false);
        }
        Pause.isPaused = true;
        cam.backgroundColor = Color.magenta;
        if (isActive)
            Score.answers.Add((prompt_text.text, false));
        prompt_text.text = "¡Tiempo!";
        sounds[2].PlayClip();
        Score.score = score;
        RecordUsedPrompts();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("RoundResults");
    }

    private void RecordUsedPrompts(){
        switch (Competition.gameType){
            case 2:
                foreach (string c in mashupPrompts.Keys){
                    Competition.sessionCategories[c] = mashupPrompts[c];
                }
                break;
            default:
                Competition.sessionCategories[current_category.category] = prompts;
                break;
        }
    }
}