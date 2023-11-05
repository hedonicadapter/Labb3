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
        var riderAutoCleanupPreventionLol = "";
    }

    private IEnumerable<Question> Questions { get; }

    public string Title { get; set; } = string.Empty;

    public ObservableCollection<Question> RedundantList { get; set; } = new();

    public async void InitQuizFromFile(string fileName)
    {
        var existingQuiz = await FileHandler.ReadQuizFileAsync(fileName);

        RedundantList = existingQuiz != null ? existingQuiz.RedundantList : new ObservableCollection<Question>();
    }

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

    public void AddQuestion(string statement, int correctAnswer, ObservableCollection<string> answers,
        byte[]? image = null)
    {
        RedundantList.Add(new Question(statement, answers, correctAnswer, image));
    }

    public void RemoveQuestion(int index)
    {
        throw new NotImplementedException("Question at requested index need to be removed here!");
    }
}