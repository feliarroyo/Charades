using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    public enum GameModes{
        QuickPlay,
        Competition,
        MashUp,
        Default
    };

    public const string 
    PREF_TEAM_COUNT = "teams",
    PREF_TEAM1 = "Team1",
    PREF_TEAM2 = "Team2",
    PREF_SHOWSCREENBUTTONS = "showScreenButtons",
    PREF_USEMOTIONCONTROLS = "useMotionControls",
    PREF_WAITDURATION = "answerWaitDuration",
    MASHUP_NAME = "Mash-Up", 
    MASHUP_DESC = "¡Pueden tocar enunciados de cualquiera de las categorías seleccionadas!", 
    MASHUP_ICON = "pregunta",
    NOTEAM = "que adivines",
    DEFAULT_TEAM1 = "Equipo 1",
    DEFAULT_TEAM2 = "Equipo 2",
    // Scenes
    SCENE_PRESENT = "Presentation",
    SCENE_CHARADES = "CharadesGameplay",
    SCENE_FINALRESULTS = "FinalResults"
    ;
    
}
