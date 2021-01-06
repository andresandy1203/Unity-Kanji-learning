using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kanji", menuName = "Lesson/new Kanji")]
public class Kanji : ScriptableObject
{
    //Set up kanji serializable fields
    [SerializeField] private String _info = String.Empty;
    public String Info { get { return _info; } }
    [SerializeField] private String _english = String.Empty;
    public String English { get { return _english; } }
    [SerializeField] private String _onyomi = String.Empty;
    public String Onyomi { get { return _onyomi; } }
    [SerializeField] private String _mnemonic = String.Empty;
    public String Mnemonic { get { return _mnemonic; } }
    [SerializeField] private String _kunyomi = String.Empty;
    public String Kunyomi { get { return _kunyomi; } }
    [SerializeField] private String _jukugo = String.Empty;
    public String Jukugo { get { return _jukugo; } }
    [SerializeField] private String _lookalikes = String.Empty;
    public String Lookalikes { get { return _lookalikes; } }


}

