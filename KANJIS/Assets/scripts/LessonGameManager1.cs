using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class LessonGameManager1 : MonoBehaviour
{
    private Kanji[] _kanji = null;
    public Kanji[] Kanjis { get { return _kanji; } }

    [SerializeField] TextMeshProUGUI countText = null;
    [SerializeField] LessonGameEvents events = null;
    private List<int> FinishedKanjis = new List<int>();
    private int currentKanji = 0;

    //Check if all kanjis in lesson have been viewed
    private bool IsLessonFinished
    {
        get
        {
            return (FinishedKanjis.Count < Kanjis.Length) ? false : true;
        }
    }

    

    /* Start is called before the first frame update */

    void Start()
    {
        
    }
     void Update()
    {
        //Load kanji list specific to each scene by name
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("NumbersLesson"))
        {
            LoadKanjis();
            
        }

        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TimeLesson"))
        {
            LoadTimeKanjis();
            
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("PeopleLesson"))
        {
            LoadPeopleKanjis();
            
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("PlacesLesson"))
        {
            LoadPlacesKanjis();

        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("VerbsLesson"))
        {
            LoadVerbsKanjis();

        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("AdjectivesLesson"))
        {
            LoadAdjectivesKanjis();

        }
        Display();
    }
    public void LoadLessons()
    {
        SceneManager.LoadScene("N5Scene", LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Display()
    {
        var kanji = Kanjis[currentKanji];
        var count = FinishedKanjis.Count+1;
        countText.text = (count.ToString() +"/" + Kanjis.Length.ToString());
        if (events.UpdateKanjiUI != null)
        {
            events.UpdateKanjiUI(kanji);
        }
        else { Debug.LogWarning("Ups! Something went wrong while trying to display new Kanji UI Data. LessonsGameEvents.UpdateKanjiUI is null. Issue occured in LessonsGameManager.Display() method."); }


    }
    public void Accept()
    {
        //Check if button will return to menu or load next kanji
        FinishedKanjis.Add(currentKanji);
        if (IsLessonFinished)
        {

            SceneManager.LoadScene("N5Scene", LoadSceneMode.Single);
            //return to menu screen
        }
        else
        {
           
            currentKanji += 1;
            Display();
        }
        

    }

    public void GoBack()
    {
        //Load previous Kanji or main menu
        if (currentKanji==0)
        {
            SceneManager.LoadScene("N5Scene", LoadSceneMode.Single);
            
        }
        else
        {
            FinishedKanjis.Remove(currentKanji - 1);
            currentKanji -= 1;
            Display();
        }
    }
    
    //Load kanji specific to each lesson from resources directory
    void LoadKanjis()
    {
        Object[] objs = Resources.LoadAll("Kanjis", typeof(Kanji));
        
        _kanji = new Kanji[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            _kanji[i] = (Kanji)objs[i];
        }
    }
    void LoadTimeKanjis()
    {
        Object[] objs = Resources.LoadAll("TimeKanjis", typeof(Kanji));

        _kanji = new Kanji[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            _kanji[i] = (Kanji)objs[i];
        }
    }
    void LoadPeopleKanjis()
    {
        Object[] objs = Resources.LoadAll("PeopleKanjis", typeof(Kanji));

        _kanji = new Kanji[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            _kanji[i] = (Kanji)objs[i];
        }
    }
    void LoadPlacesKanjis()
    {
        Object[] objs = Resources.LoadAll("PlacesKanjis", typeof(Kanji));

        _kanji = new Kanji[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            _kanji[i] = (Kanji)objs[i];
        }
    }
    void LoadVerbsKanjis()
    {
        Object[] objs = Resources.LoadAll("VerbsKanjis", typeof(Kanji));

        _kanji = new Kanji[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            _kanji[i] = (Kanji)objs[i];
        }
    }
    void LoadAdjectivesKanjis()
    {
        Object[] objs = Resources.LoadAll("AdjectivesKanjis", typeof(Kanji));

        _kanji = new Kanji[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            _kanji[i] = (Kanji)objs[i];
        }
    }
    
}
