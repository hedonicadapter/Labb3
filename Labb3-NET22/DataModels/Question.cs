using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Labb3_NET22.DataModels;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class Question : INotifyPropertyChanged
{
    private string[]? _answers;
    private byte[]? _image;


    public Question(string statement,
        string[] answers,
        int correctAnswer, byte[]? image)
    {
        Statement = statement;
        Answers = answers;
        CorrectAnswer = correctAnswer;
        AttemptedAnswer = -1;
        Image = image;
    }

    public Question()
    {
    }

    public string? Statement { get; set; }
    public string? Tag { get; set; }

    public byte[]? Image
    {
        get => _image;
        set => SetField(ref _image, value);
    }

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