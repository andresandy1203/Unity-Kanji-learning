using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvents", menuName = "Quiz/new GameEvents")]
public class GameEvents : ScriptableObject
{
    //Set uo Game evens
    public delegate void UpdateQuestionUICallback(Question question);
    public UpdateQuestionUICallback UpdateQuestionUI =null;
    

    public delegate void UpdateQuestionAnswerCallBack(AnswerData pickedAnswer);
    public UpdateQuestionAnswerCallBack UpdateQuestionAnswer=null;

    public delegate void DisplayResolutionScreenCallBack(UIManager.ResolutionScreenType type, int score);
    public DisplayResolutionScreenCallBack DisplayResolutionScreen=null;

    public delegate void ScoreUpdateCallback();
    public ScoreUpdateCallback ScoreUpdated=null;

    [HideInInspector]
    public int level = 1;
    public const int maxLevel = 7;
    [HideInInspector]
    public int CurrentFinalScore;
    [HideInInspector]
    public int StartupHighscore;
}
