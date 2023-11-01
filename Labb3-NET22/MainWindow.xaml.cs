using System.Windows;
using Labb3_NET22.DataModels;
using Labb3_NET22.Helpers;

namespace Labb3_NET22;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly WindowHandler _windows;

    public MainWindow()
    {
        InitializeComponent();
        var question = new Question
        {
            Statement = "hello",
            Answers = new[] { "" },
            CorrectAnswer = 0
        };

        _windows = new WindowHandler();
        DataContext = question;
    }

    private void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        _windows.ShowWindow("Play");
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        _windows.ShowWindow("Edit");
    }

    private void CreateButton_Click(object sender, RoutedEventArgs e)
    {
        _windows.ShowWindow("Create");
    }
}