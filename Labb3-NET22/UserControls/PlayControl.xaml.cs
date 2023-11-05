using System.Windows;
using System.Windows.Controls;
using Labb3_NET22.DataModels;

namespace Labb3_NET22.UserControls;

public partial class PlayControl : UserControl
{
    public PlayControl(Quiz quiz)
    {
        Context = new PlayControlDataContext(quiz);
        DataContext = Context;

        InitializeComponent();
    }

    private PlayControlDataContext Context { get; }


    private void PreviousButton_OnClick(object sender, RoutedEventArgs e)
    {
        Context.TraverseQuestions(false);
    }

    private void NextButton_OnClick(object sender, RoutedEventArgs e)
    {
        if ((string)NextButton.Content == "Finish")
            // Context.ShowQuizResults();
            return;

        Context.TraverseQuestions(true);
    }

    private void AnswersListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Context.CurrentQuestion.AttemptedAnswer = AnswersListBox.SelectedIndex;
    }
}