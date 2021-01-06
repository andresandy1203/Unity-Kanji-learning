using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LessonGameEvents", menuName = "Lesson/new LessonGameEvents")]
public class LessonGameEvents : ScriptableObject
{
    //Set up Update UI callbacks
    public delegate void UpdateKanjiUICallback(Kanji kanji);
    public UpdateKanjiUICallback UpdateKanjiUI = null;
}
    