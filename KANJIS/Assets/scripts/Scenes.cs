using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    //Load Lessons from name methods
   public void LoadLessons()
    {
        SceneManager.LoadScene("N5Scene", LoadSceneMode.Single);
    }
    public void LoadTitle()
    {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
    public void LoadPractice()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    public void LoadExam()
    {
       
    }
    public void LoadNumbers()
    {
        SceneManager.LoadScene("NumbersLesson", LoadSceneMode.Single);
    }
    public void LoadTime()
    {
        SceneManager.LoadScene("TimeLesson", LoadSceneMode.Single);
    }
    public void LoadPeople()
    {
        SceneManager.LoadScene("PeopleLesson", LoadSceneMode.Single);
    }
    public void LoadPlaces()
    {
        SceneManager.LoadScene("PlacesLesson", LoadSceneMode.Single);
    }
    public void LoadVerbs()
    {
        SceneManager.LoadScene("VerbsLesson", LoadSceneMode.Single);
    }
    public void LoadAdjectives()
    {
        SceneManager.LoadScene("AdjectivesLesson", LoadSceneMode.Single);
    }

}
