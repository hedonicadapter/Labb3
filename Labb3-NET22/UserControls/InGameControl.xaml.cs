using System.Windows;
using System.Windows.Controls;
using Labb3_NET22.DataModels;

namespace Labb3_NET22.UserControls;

public partial class InGameControl : UserControl
{
    public InGameControl(Quiz quiz)
    {
        Context = new InGameControlDataContext(quiz);
        DataContext = Context;

        InitializeComponent();
    }

    private InGameControlDataContext Context { get; }


    private void PreviousButton_OnClick(object sender, RoutedEventArgs e)
    {
        Context.TraverseQuestions(false);
    }

    private void NextButton_OnClick(object sender, RoutedEventArgs e)
    {
        Context.TraverseQuestions(true);
    }
}