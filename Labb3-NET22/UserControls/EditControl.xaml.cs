using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Labb3_NET22.DataModels;
using Labb3_NET22.Helpers;
using Microsoft.Win32;

namespace Labb3_NET22;

public partial class EditControl : UserControl
{
    public EditControl(Quiz selectedQuiz)
    {
        Context = new EditControlDataContext(selectedQuiz);
        DataContext = Context;

        InitializeComponent();
    }

    protected EditControlDataContext Context { get; set; }

    protected void AddQuestionButton_OnClick(object sender, RoutedEventArgs e)
    {
        // Context.CurrentQuiz.AddQuestion(Context.CurrentQuestion.Statement, Context.CurrentQuestion.CorrectAnswer,
        //     Context.CurrentQuestion.Answers, Context.CurrentQuestion.Image);
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
        Context.CurrentlySelected = PossibleAnswersDataGrid.SelectedIndex;
    }

    protected void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        var containerWindow = Window.GetWindow(this);
        containerWindow?.Hide();
    }

    protected void SaveQuizButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (Context.CurrentQuestion.Answers == null || Context.CurrentQuestion.Answers.Length < 1)
        {
            MessageBox.Show("Please add at least one answer."); // Borde vara two answers egentligen men w/e
            return;
        }

        if (Context.CurrentQuestion.CorrectAnswer is null or -1)
        {
            MessageBox.Show("Please select the correct answer.");
            return;
        }


        FileHandler.SaveQuiz(Context.CurrentQuiz);
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