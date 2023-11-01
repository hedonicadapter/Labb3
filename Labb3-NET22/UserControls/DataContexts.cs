using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Labb3_NET22.DataModels;
using Labb3_NET22.Helpers;

namespace Labb3_NET22;

public class CreateControlDataContext
{
    public Quiz CurrentQuiz { get; } = new();
    public Question CurrentQuestion { get; } = new();
}

public class PlayControlDataContext
{
    public PlayControlDataContext()
    {
        Quizzes = new ObservableCollection<Quiz>(FileHandler.ReadQuizFiles());
    }

    public ObservableCollection<Quiz> Quizzes { get; }
}

public class InGameControlDataContext : INotifyPropertyChanged
{
    private Question? _currentQuestion;

    public InGameControlDataContext(Quiz selectedQuiz)
    {
        CurrentQuiz = selectedQuiz;
        CurrentQuiz.RandomizeQuestions();

        CurrentQuestion = CurrentQuiz.RedundantList[0];
    }

    public InGameControlDataContext()
    {
    }

    public Quiz CurrentQuiz { get; }

    public Question CurrentQuestion
    {
        get => _currentQuestion;
        private set => SetField(ref _currentQuestion, value);
    }

    private int CurrentQuestionIndex { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void TraverseQuestions(bool goNext)
    {
        var max = CurrentQuiz.RedundantList.Count;

        if (goNext)
        {
            var newIndex = CurrentQuestionIndex + 1;

            if (newIndex < max) CurrentQuestionIndex = newIndex;
        }
        else
        {
            var newIndex = CurrentQuestionIndex - 1;

            if (newIndex < max && newIndex >= 0) CurrentQuestionIndex = newIndex;
        }

        CurrentQuestion = CurrentQuiz.RedundantList[CurrentQuestionIndex];
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}