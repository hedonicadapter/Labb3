using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Labb3_NET22.Helpers;

namespace Labb3_NET22.DataModels;

public class Quiz
{
    public Quiz()
    {
        Questions = new List<Question>();
        RedundantList = new ObservableCollection<Question>();
    }

    public Quiz(string fileName)
    {
        var existingQuiz = FileHandler.ReadQuizFile(fileName);

        if (existingQuiz != null)
        {
            Questions = existingQuiz.Questions;
            RedundantList = existingQuiz.RedundantList;
        }
        else
        {
            Questions = new List<Question>();
            RedundantList = new ObservableCollection<Question>();
        }
    }

    public IEnumerable<Question> Questions { get; }

    public string Title { get; set; } = string.Empty;


    public ObservableCollection<Question> RedundantList { get; set; }

    public void RandomizeQuestions()
    {
        var rand = new Random();
        RedundantList = new ObservableCollection<Question>(RedundantList.OrderBy(question => rand.Next()));
    }

    public Question GetRandomQuestion()
    {
        var rand = new Random();
        var index = rand.Next(0, RedundantList.Count);

        return RedundantList[index];
    }

    public void AddQuestion(string statement, int correctAnswer, params string[] answers)
    {
        RedundantList.Add(new Question { Statement = statement, Answers = answers, CorrectAnswer = correctAnswer });
    }

    public void RemoveQuestion(int index)
    {
        throw new NotImplementedException("Question at requested index need to be removed here!");
    }
}