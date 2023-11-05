using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Labb3_NET22.DataModels;
using Labb3_NET22.Helpers;
using Xceed.Wpf.Toolkit;

namespace Labb3_NET22;

public class MainWindowDataContext
{
}

public class CreateControlDataContext
{
    public Quiz CurrentQuiz { get; } = new();
    public Question CurrentQuestion { get; } = new();
}

public class MainControlDataContext
{
    private MainControlDataContext()
    {
    }

    public ObservableCollection<Quiz>? Quizzes { get; private set; }

    public static async Task<MainControlDataContext> AsyncConstructor()
    {
        var instance = new MainControlDataContext();

        try
        {
            var quizzesFromFiles = await FileHandler.ReadQuizFiles();

            instance.Quizzes = quizzesFromFiles != null
                ? new ObservableCollection<Quiz>(quizzesFromFiles!)
                : null;
        }
        catch (Exception e)
        {
            MessageBox.Show($"Something went wrong creating main context: {e}");
        }

        return instance;
    }
}

public class PlayControlDataContext : INotifyPropertyChanged
{
    private string _buttonText = "Next";
    private int _correctAnswers;
    private Question? _currentQuestion;
    private int _currentQuestionIndex;


    public PlayControlDataContext(Quiz selectedQuiz)
    {
        CurrentQuiz = selectedQuiz;
        CurrentQuiz.RandomizeQuestions();
        CorrectAnswers = 0;

        CurrentQuestion = CurrentQuiz.RedundantList[0];
    }

    public int CorrectAnswers
    {
        get => _correctAnswers;
        private set => SetField(ref _correctAnswers, value);
    }

    public Quiz CurrentQuiz { get; }

    public string ButtonText
    {
        get => _buttonText;
        private set => SetField(ref _buttonText, value);
    }

    public Question? CurrentQuestion
    {
        get => _currentQuestion;
        private set => SetField(ref _currentQuestion, value);
    }

    private int CurrentQuestionIndex
    {
        get => _currentQuestionIndex;
        set
        {
            _currentQuestionIndex = value;

            ButtonText = value == CurrentQuiz.RedundantList.Count - 1 ? "Finish" : "Next";
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void CountCorrect()
    {
        CorrectAnswers =
            CurrentQuiz.RedundantList.Count(question => question.AttemptedAnswer == question.CorrectAnswer);

        // MessageBox.Show();
    }

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

        CountCorrect();
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

public class EditControlDataContext : CreateControlDataContext
{
}