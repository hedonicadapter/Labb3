using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Labb3_NET22.Helpers;

namespace Labb3_NET22;

public partial class CreateControl : UserControl
{
    public CreateControl()
    {
        InitializeComponent();

        DataContext = Context;
    }

    private CreateControlDataContext Context { get; } = new();

    private void AddQuestionButton_OnClick(object sender, RoutedEventArgs e)
    {
        Context.CurrentQuiz.AddQuestion(Context.CurrentQuestion.Statement, Context.CurrentQuestion.CorrectAnswer,
            Context.CurrentQuestion.Answers);
    }

    private void AddAnswerButton_OnClick(object sender, RoutedEventArgs e)
    {
        var currentAnswers = new List<string>(Context.CurrentQuestion.Answers ?? new[] { "" });
        var newAnswer = AnswerTextBox.Text;
        currentAnswers.Add(newAnswer);

        Context.CurrentQuestion.Answers = currentAnswers.ToArray();
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Context.CurrentQuestion.CorrectAnswer = PossibleAnswersListBox.SelectedIndex;
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        var containerWindow = Window.GetWindow(this);
        containerWindow?.Hide();
    }

    private void CreateQuizButton_OnClick(object sender, RoutedEventArgs e)
    {
        FileHandler.SaveQuiz(Context.CurrentQuiz);
    }

    private void QuizTitleTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        Context.CurrentQuiz.Title = QuizTitleTextBox.Text;
    }
}