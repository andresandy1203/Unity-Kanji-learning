using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

[Serializable()]
public struct LessonUIElements
{
    
    //UI elements setup
    [SerializeField] TextMeshProUGUI Kanji;
    public TextMeshProUGUI Kanjitxt { get { return Kanji; } }

    [SerializeField] TextMeshProUGUI English;
    public TextMeshProUGUI Englishtxt { get { return English; } }

    [SerializeField] TextMeshProUGUI Onyomi;
    public TextMeshProUGUI Onyomitxt { get { return Onyomi; } }

    [SerializeField] TextMeshProUGUI Mnemonic;
    public TextMeshProUGUI Mnemonictxt { get { return Mnemonic; } }

    [SerializeField] TextMeshProUGUI Kunyomi;
    public TextMeshProUGUI Kunyomitxt { get { return Kunyomi; } }

    [SerializeField] TextMeshProUGUI Jukugo;
    public TextMeshProUGUI Jukugotxt { get { return Jukugo; } }

    [SerializeField] TextMeshProUGUI Lookalikes;
    public TextMeshProUGUI Lookaliketxt { get { return Lookalikes; } }




}
public class LessonUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LessonGameEvents events = null;

    [Header("UI Elements (Prefabs)")]
    [SerializeField] LessonUIElements luIElements = new LessonUIElements();

    //Update Kanji UI onEnable & onDisable
    void OnEnable()
    {
        events.UpdateKanjiUI += UpdateKanjiUI;
    }
    private void OnDisable()
    {
        events.UpdateKanjiUI -= UpdateKanjiUI;
    }

    //Setup text in UI from kanji object strings
    void UpdateKanjiUI(Kanji kanji)
    {
        luIElements.Kanjitxt.text = kanji.Info;
        luIElements.Englishtxt.text = kanji.English;
        luIElements.Onyomitxt.text = kanji.Onyomi;
        luIElements.Mnemonictxt.text = kanji.Mnemonic;
        luIElements.Kunyomitxt.text = kanji.Kunyomi;
        luIElements.Jukugotxt.text = kanji.Jukugo;
        luIElements.Lookaliketxt.text = kanji.Lookalikes;
    }
}


