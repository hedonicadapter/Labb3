using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Labb3_NET22.DataModels;
using Labb3_NET22.Helpers;

namespace Labb3_NET22;

public partial class PlayControl : UserControl
{
    public PlayControl()
    {
        DataContext = Context;

        InitializeComponent();
    }

    private PlayControlDataContext Context { get; } = new();

    private void PlayButton_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedQuiz = (Quiz)QuizSelectionGrid.SelectedItem;

        if (selectedQuiz == null)
        {
            MessageBox.Show("Select a quiz first idiota");
            return;
        }

        WindowHandler.StartQuiz(selectedQuiz);
    }

    private void QuizSelectionGrid_OnPreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (QuizSelectionGrid.SelectedItem == null) return;

        // Chat-GPT
        // Check if an item (row) was double-clicked
        var row =
            ItemsControl.ContainerFromElement(QuizSelectionGrid, e.OriginalSource as DependencyObject) as DataGridRow;

        if (row != null) PlayButton_OnClick(sender, e);
    }
}