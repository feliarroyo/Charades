using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.AdaptivePerformance.Google.Android;

/// <summary>
/// This class manages elements related to gameplay within a single round.
/// </summary>
public class RoundGameplay : MonoBehaviour
{
    
    public static Camera cam; // Camera's background color is changed depending on game status
    public static IPromptPool prompts;
    
    private bool isPromptOnScreen = true; // used to prevent multiple scoring/skipping at once
    public TextMeshProUGUI mainText; // contains text used for prompts and game changes
    public TextMeshProUGUI hitsText; // contains text showing points obtained
    // public TextMeshProUGUI gyroTest;
    public SoundEffectPlayer[] sounds; // 0 = correct, 1 = pass prompt, 2 = finish
    public Timer timer;
    public bool isRoundOver = false;

    // Start is called before the first frame update
    void Start()
    {
        Config.GameplayConfig();
        Pause.isPaused = false;
        Score.ClearScore();
        cam = FindObjectOfType<Camera>().GetComponent<Camera>();
        mainText = GameObject.Find("Prompt").GetComponent<TextMeshProUGUI>();
        hitsText = GameObject.Find("Points").GetComponent<TextMeshProUGUI>();
        timer = GameObject.Find("Time").GetComponent<Timer>();
        timer.SetTime(PlayerPrefs.GetInt(Const.PREF_ROUNDDURATION, 60));
        prompts = Competition.GetPromptPool();
        prompts.InitializePrompts();
        GetNewPrompt();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.isPaused)
            return;
        else if (timer.TimeIsUp())
            StartCoroutine(EndGame());
        
        if (!isPromptOnScreen | isRoundOver){
            return;
        }

        // Windows/Editor controls
        #if UNITY_EDITOR
        if (Input.GetButton("Pass"))
            AnswerPrompt(true);
        else if (Input.GetButton("Fail"))
            AnswerPrompt(false);
        #endif

        // Android controls
        #if UNITY_ANDROID
        if (PlayerPrefs.GetInt(Const.PREF_USEMOTIONCONTROLS, 1) == 1)
            CheckInclination();
        #endif
    }

    /// <summary>
    /// Checks how tilted is the device, and answers/skips the prompt at certain values.
    /// </summary>
    private void CheckInclination(){
        Vector3 tilting = new(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
        // gyroTest.text = tilting.ToString();
        if ((tilting.x > 0.3f) || (tilting.x < -0.3f) || (tilting.z > 0.9f) || (tilting.z < -0.9f))
            return;
        else if (tilting.z > 0.7f)
            AnswerPrompt(true);
        else if (tilting.z < -0.7f)
            AnswerPrompt(false);
    }

    /// <summary>
    /// Changes the text of the prompt and gives a point if needed.
    /// </summary>
    /// <param name="givePoint">Determines if a point should be assigned to the score or not.</param>
    public void AnswerPrompt(bool givePoint){
        isPromptOnScreen = false;
        if (givePoint) {
            IncreaseScore();
        }
        StartCoroutine(ChangeScreen(givePoint));
    }

    /// <summary>
    /// Removes the current prompt from the prompt pool, and return a new word within the pile.
    /// </summary>
    /// <param name="givePoint">Determines if a point should be assigned to the score or not.</param>
    private IEnumerator ChangeScreen(bool givePoint = false)
    {
        RemovePromptFromPool(givePoint);
        if (givePoint){
            SetScreen(Color.green, Const.CORRECT, sounds[0]);
        }
        else{
            SetScreen(Color.red, Const.SKIP, sounds[1]);
        }
        float answerWait = PlayerPrefs.GetFloat(Const.PREF_WAITDURATION, 1f);
        yield return new WaitForSeconds(0.5f);
        hitsText.color = Color.white;
        yield return new WaitForSeconds(answerWait - 0.5f);
        GetNewPrompt();
    }

    /// <summary>
    /// Defines what to show on screen, including background color, main text, and sound to play if any.
    /// </summary>
    /// <param name="bgColor">Color to set on the background of the camera.</param>
    /// <param name="text">Text to display in the center of the screen.</param>
    /// <param name="sound">Sound to play, can be omitted if no sound will play.</param>
    private void SetScreen(Color bgColor, string text, SoundEffectPlayer sound = null){
        cam.backgroundColor = bgColor;
        mainText.text = text;
        if (sound != null)
            sound.PlayClip();
    }

    /// <summary>
    /// Registers score to show in results, and removes the current prompt from the prompt pool, to avoid repeating prompts whenever possible.
    /// </summary>
    /// <param name="wasAnswered">Indicates if the answer is registered as answered or skipped.</param>
    private void RemovePromptFromPool(bool wasAnswered){
        Score.answers.Add((mainText.text, wasAnswered));
        prompts.RemovePrompt(mainText.text);
    }

    /// <summary>
    /// Gets a new prompt to show on-screen after answering. 
    /// </summary>
    private void GetNewPrompt(){
        prompts.RestartPoolIfEmpty();
        if (!isRoundOver){
            isPromptOnScreen = true;
            SetScreen(Const.DefaultBGColor, prompts.GetNewPrompt());
        }
    }

    /// <summary>
    /// Increases round score by 1, showing it in the screen counter.
    /// </summary>
    private void IncreaseScore() 
    {
        Score.score++;
        hitsText.color = Const.CorrectTextColor;
        hitsText.text = Score.GetScoreText();
    }

    /// <summary>
    /// Ends the game.
    /// </summary>
    /// <returns></returns>
    public IEnumerator EndGame(){
        HideButtons();
        isRoundOver = true;
        Pause.isPaused = true;
        if (isPromptOnScreen) {
            RemovePromptFromPool(false);
        }
        SetScreen(Color.magenta, Const.TIME_OVER, sounds[2]);
        prompts.RecordUsedPrompts();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(Const.SCENE_ROUNDRESULTS);
    }

    /// <summary>
    /// Disables buttons shown on-screen.
    /// </summary>
    private void HideButtons(){
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Button")){
            go.SetActive(false);
        }
    }
}