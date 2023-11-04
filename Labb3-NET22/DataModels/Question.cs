using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Labb3_NET22.DataModels;

public class Question : INotifyPropertyChanged
{
    private string[]? _answers;

    public Question(string statement,
        string[] answers,
        int correctAnswer)
    {
        Statement = statement;
        Answers = answers;
        CorrectAnswer = correctAnswer;
        AttemptedAnswer = -1;
    }

    public Question()
    {
    }

    public string? Statement { get; set; }

    public string[] Answers
    {
        get => _answers;
        set => SetField(ref _answers, value);
    }

    public int CorrectAnswer { get; set; }
    public int AttemptedAnswer { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;

    // Rider generated
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Rider generated
    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}