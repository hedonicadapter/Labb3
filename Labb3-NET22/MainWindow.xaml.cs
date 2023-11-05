using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Labb3_NET22.DataModels;
using Labb3_NET22.Helpers;

namespace Labb3_NET22;

public partial class MainWindow : CustomWindow
{
    private readonly WindowHandler _windows;

    public MainWindow()
    {
        InitializeDataContext();
        InitializeComponent();
        _windows = new WindowHandler();
    }

    public MainControlDataContext Context { get; set; }

    public async void InitializeDataContext()
    {
        Context = await MainControlDataContext.AsyncConstructor();
        DataContext = Context;
    }

    private void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedQuiz = (Quiz)QuizSelectionGrid.SelectedItem;

        if (selectedQuiz == null)
        {
            MessageBox.Show("Select a quiz first idiota");
            return;
        }

        _windows.ShowWindow("Play", selectedQuiz);
    }


    private void CreateButton_OnClick(object sender, RoutedEventArgs e)
    {
        _windows.ShowWindow("Create");
    }

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

    private void EditButton_OnClick(object sender, RoutedEventArgs e)
    {
        var selectedQuiz = (Quiz)QuizSelectionGrid.SelectedItem;

        if (selectedQuiz == null)
        {
            MessageBox.Show("Select a quiz first idiota");
            return;
        }

        _windows.ShowWindow("Edit", selectedQuiz);
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