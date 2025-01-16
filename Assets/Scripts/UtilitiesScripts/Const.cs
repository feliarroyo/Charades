using UnityEngine;

public static class Const
{
    public enum GameModes
    {
        QuickPlay,
        Competition,
        MashUp,
        Default
    };
    public const string
    // PlayerPrefs
        PREF_SHOWSCREENBUTTONS = "showScreenButtons",
        PREF_USEMOTIONCONTROLS = "useMotionControls",
        PREF_WAITDURATION = "answerWaitDuration",
        PREF_TEAM_COUNT = "teams",
        PREF_TEAM1 = "Team1",
        PREF_TEAM2 = "Team2",
    // Mash-Up category values
        MASHUP_NAME = "Mash-Up",
        MASHUP_DESC = "¡Pueden tocar enunciados de cualquiera de las categorías seleccionadas!",
        MASHUP_ICON = "pregunta",
        MASHUP_ROUND_COUNT = "mashupRoundCount",
    // Gameplay strings
        CORRECT = "Bien",
        SKIP = "Paso",
        TIME_OVER = "¡Tiempo!",
        NOTEAM = "que adivines",
        DEFAULT_TEAM1 = "Equipo 1",
        DEFAULT_TEAM2 = "Equipo 2",
        DEFAULT_ICON = "default",
    // Scenes
        SCENE_MAINMENU = "MainMenu",
        SCENE_PRESENT = "Presentation",
        SCENE_CATSELECT = "CategorySelect",
        SCENE_CHARADES = "Round",
        SCENE_ROUNDRESULTS = "RoundResults",
        SCENE_FINALRESULTS = "FinalResults"
    ;
    public static Color DefaultColor = new(0.1921569f,0.3019608f,0.4745098f,1f);
}