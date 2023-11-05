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

    protected CreateControlDataContext Context { get; set; } = new();

    protected void AddQuestionButton_OnClick(object sender, RoutedEventArgs e)
    {
        var errorValue = "";

        if (Context.CurrentQuestion.Statement == null) errorValue = "a question";
        if (Context.CurrentQuestion.CorrectAnswer == null) errorValue = "a correct answer";
        if (Context.CurrentQuestion.Answers == null) errorValue = "answers";

        if (errorValue.Length > 0)
        {
            MessageBox.Show($"You forgot to set {errorValue}");
            return;
        }

        Context.CurrentQuiz.AddQuestion(Context.CurrentQuestion.Statement!, (int)Context.CurrentQuestion.CorrectAnswer,
            Context.CurrentQuestion.Answers!, Context.CurrentQuestion.Image);
    }

    protected void AddAnswerButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (AnswerTextBox.Text.Length < 1)
        {
            MessageBox.Show("Please add words to your answer smh."); // Borde vara two answers egentligen men w/e
            return;
        }

        var currentAnswers = Context.CurrentQuestion.Answers != null
            ? new List<string>(Context.CurrentQuestion.Answers)
            : new List<string>();
        var newAnswer = AnswerTextBox.Text;
        currentAnswers.Add(newAnswer);

        Context.CurrentQuestion.Answers = currentAnswers.ToArray();
    }

    protected void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Context.CurrentQuestion.CorrectAnswer = PossibleAnswersListBox.SelectedIndex;
    }

    protected void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        var containerWindow = Window.GetWindow(this);
        containerWindow?.Hide();
    }

    protected async void CreateQuizButton_OnClick(object sender, RoutedEventArgs e)
    {
        await FileHandler.SaveQuiz(Context.CurrentQuiz);
        ((MainWindow)Application.Current.MainWindow).InitializeDataContext();
    }

    protected void QuizTitleTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        Context.CurrentQuiz.Title = QuizTitleTextBox.Text;
    }

    protected void AddImageButton_OnClick(object sender, RoutedEventArgs e)
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

    protected void RemoveImageButton_OnClick(object sender, RoutedEventArgs e)
    {
        Context.CurrentQuestion.Image = null;
        ImageElement.Source = null;
    }
}