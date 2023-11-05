using System;
using System.Collections.ObjectModel;
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

    private EditControlDataContext Context { get; }

    private void AddQuestionButton_OnClick(object sender, RoutedEventArgs e)
    {
        // Context.CurrentQuiz.AddQuestion(Context.CurrentQuestion.Statement, Context.CurrentQuestion.CorrectAnswer,
        //     Context.CurrentQuestion.Answers, Context.CurrentQuestion.Image);
    }

    private void AddAnswerButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (AnswerTextBox.Text.Length < 1)
        {
            MessageBox.Show("Please add words to your answer smh."); // Borde vara two answers egentligen men w/e
            return;
        }

        var currentAnswers = Context.CurrentQuestion.Answers ?? new ObservableCollection<string>();
        var newAnswer = AnswerTextBox.Text;
        currentAnswers.Add(newAnswer);

        Context.CurrentQuestion.Answers = currentAnswers;
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Context.CurrentlySelected = PossibleAnswersDataGrid.SelectedIndex;
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        var containerWindow = Window.GetWindow(this);
        containerWindow?.Hide();
    }

    private void SaveQuizButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (Context.CurrentQuestion.Answers == null || Context.CurrentQuestion.Answers.Count < 1)
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

    private void QuizTitleTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        Context.CurrentQuiz.Title = QuizTitleTextBox.Text;
    }

    private void AddImageButton_OnClick(object sender, RoutedEventArgs e)
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

    private void RemoveImageButton_OnClick(object sender, RoutedEventArgs e)
    {
        Context.CurrentQuestion.Image = null;
        ImageElement.Source = null;
    }

    private void PossibleAnswersDataGrid_OnCellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
    {
        if (e.EditAction == DataGridEditAction.Commit)
        {
            var editedCell = e.EditingElement as TextBox;
            var newValue = editedCell.Text;
            var index = PossibleAnswersDataGrid.Items.IndexOf(e.Row.Item);

            if (index >= 0 && index < Context.CurrentQuestion.Answers.Count)
                Context.CurrentQuestion.Answers[index] =
                    newValue;
        }
    }
}