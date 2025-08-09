using UnityEngine;

/// <summary>
/// This class contains constants used through the entire code.
/// </summary>
public static class Const
{
    public enum GameModes
    {
        QuickPlay,
        Competition,
        MashUp,
        Default
    };
    // PlayerPrefs
    public const string PREF_SHOWSCREENBUTTONS = "showScreenButtons";
    public const string PREF_USE_CLICK_CONTROLS = "useClickControls";
    public const string PREF_USEMOTIONCONTROLS = "useMotionControls";
    public const string PREF_MUSICVOLUME = "musicVolume";
    public const string PREF_SOUNDVOLUME = "soundVolume";
    public const string PREF_ROUNDDURATION = "roundDuration";
    public const string PREF_WAITDURATION = "answerWaitDuration";
    public const string PREF_TEAM_COUNT = "teams";
    public const string PREF_TEAM1 = "Team1";
    public const string PREF_TEAM2 = "Team2";
    // Mash-Up category values
    public const string MASHUP_NAME = "Mash-Up";
    public const string MASHUP_DESC = "¡Pueden tocar enunciados de cualquiera de las categorías seleccionadas!";
    public const string MASHUP_ICON = "pregunta";
    public const string MASHUP_ROUND_COUNT = "mashupRoundCount";
    // Gameplay strings
    public const string CORRECT = "Bien";
    public const string SKIP = "Paso";
    public const string TIME_OVER = "¡Tiempo!";
    public const string NOTEAM = "que adivines";
    public const string DEFAULT_TEAM1 = "Equipo 1";
    public const string DEFAULT_TEAM2 = "Equipo 2";
    public const string DEFAULT_ICON = "default";
    // Scenes
    public const string SCENE_MAINMENU = "MainMenu";
    public const string SCENE_PRESENT = "Presentation";
    public const string SCENE_CATSELECT = "CategorySelect";
    public const string SCENE_CHARADES = "Round";
    public const string SCENE_ROUNDRESULTS = "RoundResults";
    public const string SCENE_FINALRESULTS = "FinalResults";
    public static string customDirectory = Application.persistentDataPath + "/customCategories";
    public static Color DefaultBGColor = new(0.1921569f, 0.3019608f, 0.4745098f, 1f);
    public static Color CorrectTextColor = new(255, 187, 0, 255);
    public static Color TeamsEnabledTextColor = new(0.67f,0.286f,0.023f,1f);
    public static Color TeamsDisabledTextColor = new(0.4f,0.4f,0.4f,1f);
}