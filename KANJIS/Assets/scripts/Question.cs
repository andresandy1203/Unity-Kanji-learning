
using System.Collections.Generic;
using System;

public enum AnswerType { Multi, Single }
[Serializable()]
public class Answer
{
    public string Info = string.Empty;
    public bool IsCorrect = false;

    public Answer() { }
   
}

public class Question 
{
   
    public string Info = null;
    public Answer[] Answers = null;
    public Boolean UseTimer = false;
    public Int32 Timer = 0;
    public AnswerType Type = AnswerType.Single;
    public Int32 AddScore = 0;
    public Question() { }

    //Get list of correct answers method
    public List<int> GetCorrectAnswes()
    {
        List<int> CorrectAnswers = new List<int>();
        for (int i = 0; i < Answers.Length; i++)
        {
            if(Answers[i].IsCorrect)
            {
                CorrectAnswers.Add(i);
            }
        }
        return CorrectAnswers;
    }

}
