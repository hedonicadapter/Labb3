using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Labb3_NET22.Helpers;
using Microsoft.Win32;

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

    private async void AddImageButton_OnClick(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Image Files | *.png; *.jpg; *.jpeg; *.bmp"
        };

        if (openFileDialog.ShowDialog() != true) return;

        var imagePath = openFileDialog.FileName;

        try
        {
            var bmp = new BitmapImage(new Uri(imagePath));
            ImageElement.Source = bmp;

            var imageBytes = FileHandler.ConvertBmpToBytes(bmp);

            Context.CurrentQuestion.Image = imageBytes;
        }
        catch (Exception err)
        {
            MessageBox.Show($"Something went wrong setting your image: {err}");
        }

        ;
    }
}