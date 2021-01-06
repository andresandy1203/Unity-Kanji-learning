using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;


public class gameManager : MonoBehaviour
{
    
    //Set up objects
    private Data data = new Data();

    [SerializeField] GameEvents events = null;

    [SerializeField] Animator timerAnimator = null;
    [SerializeField] TextMeshProUGUI timerText = null;
    [SerializeField] TextMeshProUGUI nextText = null;

    [SerializeField] Color timerHalf = Color.yellow;
    [SerializeField] Color timeIsUp = Color.red;

    private List<AnswerData> PickedAnswers = new List<AnswerData>();
    private List<int> FinishedQuestions = new List<int>();
    

    private int currentQuestion = 0;
    

    private int timerStateParaHash = 0;

    private IEnumerator IE_WaitTillNextRound = null;
    private IEnumerator IE_StartTimer = null;

    private Color timerDefaultColor = Color.white;


    //Check if all questions have been answered
    private bool IsFinished
    {
        get
        {
            return (FinishedQuestions.Count < data.Questions.Length) ? false : true;
        }
    }
    

    //Update answers data
    void OnEnable()
    {
        events.UpdateQuestionAnswer += UpdateAnswers;
    }
     void OnDisable()
    {
        events.UpdateQuestionAnswer -= UpdateAnswers;
    }

    //ResetFinal Score if its the first level
     void Awake()
    {
        if (events.level == 1) { events.CurrentFinalScore = 0; }
        
    }

    void Start()
    {
        //Get HighScore
        events.StartupHighscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);

        //SEtup timer
        timerDefaultColor = timerText.color;
        LoadData();
        
        
        timerStateParaHash = Animator.StringToHash("TimerState");

        var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        UnityEngine.Random.InitState(seed);

      
        Display();
    }

    public void UpdateAnswers(AnswerData newAnswer)
    {
        //Update answers in random order
        if(data.Questions[currentQuestion].Type == AnswerType.Single)
        {
            foreach(var answer in PickedAnswers)
            {
                if(answer  != newAnswer)
                {
                    answer.Reset();
                }
                
            }
            PickedAnswers.Clear();
            PickedAnswers.Add(newAnswer);
        }
        else
        {
            bool alreadyPicked = PickedAnswers.Exists(x => x == newAnswer);
            if(alreadyPicked)
            {
                PickedAnswers.Remove(newAnswer);
            }
            else
            {
                PickedAnswers.Add(newAnswer);
            }
        }
    }

    public void EraseAnsers()
    {
        //Reset answers
        PickedAnswers = new List<AnswerData>();
    }

    //Display method
    void  Display()
    {
        EraseAnsers();
        var question = GetRandomQuestion();

        //Call for update UI
        if(events.UpdateQuestionUI != null)
        {
           events.UpdateQuestionUI(question);
        }else { Debug.LogWarning("Ups! Something went wron while trying to display Question UI Data");}

        //Set up timer
        if(question.UseTimer)
        {
            UpdateTimer(question.UseTimer);
        }
    }
    public void LoadTitle()
     {
        //Check if all levels have been played
        if (events.level != 1) { events.level -= 1;  }
        //Load title scene 
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }

    //Submit answer method
    public void Accept()
    {
      
        UpdateTimer(false);
        bool isCorrect = CheckAnswers();
        FinishedQuestions.Add(currentQuestion);

        //Addup score
        UpdateScore((isCorrect)? data.Questions[currentQuestion].AddScore : -data.Questions[currentQuestion].AddScore);

        //Load next level if all questions from current level have been answered
        //If its last level, ask if user wants to play again
        if (IsFinished)
        {
            events.level++;
            if(events.level>GameEvents.maxLevel)
            {
                events.level =1;
                nextText.text = "Play From level 1?";
            }
            SetHighscore();
        }

        //Set up UI depending if answe was correct/incorrect or if it was last answer
        var type
            = (IsFinished)
            ? UIManager.ResolutionScreenType.Finish
            : (isCorrect) ? UIManager.ResolutionScreenType.Correct
            : UIManager.ResolutionScreenType.Incorrect;

      
            events.DisplayResolutionScreen(type, data.Questions[currentQuestion].AddScore);
        
        //If it was the last answer go to next level
        if(type !=UIManager.ResolutionScreenType.Finish)
        {
            if (IE_WaitTillNextRound != null)
            {
                StopCoroutine(IE_WaitTillNextRound);
            }
            IE_WaitTillNextRound = WaitTillNextRound();
            StartCoroutine(IE_WaitTillNextRound);
        }
        

    }

    //Update timer
    void UpdateTimer(bool state)
    {
        switch(state)
        {
            case true:
                IE_StartTimer = StartTimer();
                StartCoroutine(IE_StartTimer);

                timerAnimator.SetInteger(timerStateParaHash, 2);
                break;
            case false:
                if (IE_StartTimer!=null)
                {

                    StopCoroutine(IE_StartTimer);
                }
                timerAnimator.SetInteger(timerStateParaHash, 1);
                break;
        }
    }

    //Start timer
    IEnumerator StartTimer()
    {
        var totalTime = data.Questions[currentQuestion].Timer;
        var timeLeft = totalTime;

        timerText.color = timerDefaultColor;

        //Change color of timer depending if time is near to end
        while(timeLeft > 0)
        {
            timeLeft--;

            if(timeLeft < totalTime/2 && timeLeft > totalTime/4)
            {
                timerText.color = timerHalf;

            }
            if(timeLeft < totalTime/4)
            {
                timerText.color = timeIsUp;
            }
            timerText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        //If time is up go to next question
        Accept();
    }

    IEnumerator WaitTillNextRound()
    {
        yield return new WaitForSeconds(GameUtility.ResolutionDelaytime);
        Display();
    }

   
    //Return false or true if answer was correct
    bool CheckAnswers()
    {
        if (!CompareAnswers())
        {
            return false;
        }
        return true;
    }

   
    bool CompareAnswers()
    {
        //Compare answers to correct answers list for current question
        if (PickedAnswers.Count>0)
        {
            List<int> correctAnswersList = data.Questions[this.currentQuestion].GetCorrectAnswes();
            List<int> pickedAnswer = PickedAnswers.Select(x => x.AnswerIndex).ToList();

            //Check if the correct answer matches the selected answer
            var correctAnswersConpare = correctAnswersList.Except(pickedAnswer).ToList();
            var pickedAnswerCompare = pickedAnswer.Except(correctAnswersList).ToList();
            //return if comparison was true or false
            return !correctAnswersConpare.Any() && !pickedAnswerCompare.Any();
        }
        //Return false if none was selected
        return false;
    }

    //Load XML Data from file
    void LoadData()
    {
        var path = Path.Combine(GameUtility.FileDir, GameUtility.FileName+events.level+".xml");
        data = Data.Fetch(path);
      
    }
    
    //Restart game
    public void RestartGame()
    {
        
        
        if (events.level == 1) { events.CurrentFinalScore = 0; }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    

    //Setup HighScore
    private void SetHighscore()
    {
        var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);
        if(highscore<events.CurrentFinalScore)
        {
            PlayerPrefs.SetInt(GameUtility.SavePrefKey, events.CurrentFinalScore);
        }
    }

    //Upadre Score
    private void UpdateScore(int add)
    {
        events.CurrentFinalScore += add;
        events.ScoreUpdated?.Invoke();
      
    }

    //Get Random questions
    Question GetRandomQuestion()
    {
        var randomIndex = GetRandomQuestionIndex();
        currentQuestion = randomIndex;

        return data.Questions[currentQuestion];
    }
    int GetRandomQuestionIndex()
    {
        var random = 0;
        if (FinishedQuestions.Count < data.Questions.Length)
        {
            do
            {
                random = UnityEngine.Random.Range(0, data.Questions.Length);
            } while (FinishedQuestions.Contains(random) || random == currentQuestion);
        }
        return random;
    }

}
