using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Labb3_NET22.DataModels;
using Labb3_NET22.UserControls;

namespace Labb3_NET22.Helpers;

public class WindowHandler
{
    public WindowHandler()
    {
        var windows = InitWindows();

        foreach (var window in windows) ControlWindows.Add(window.Name, window);
    }

    private Dictionary<string, CustomWindow> ControlWindows { get; } = new();

    private CustomWindow[] InitWindows()
    {
        UserControl createQuizControl = new CreateControl();
        UserControl editQuizControl = new EditControl();
        UserControl playQuizControl = new PlayControl();

        var createWindow = new CustomWindow
        {
            Title = "Create quiz",
            Name = "Create",
            Content = createQuizControl
        };

        var editWindow = new CustomWindow
        {
            Title = "Edit quiz",
            Name = "Edit",
            Content = editQuizControl
        };

        var playWindow = new CustomWindow
        {
            Title = "Play",
            Name = "Play",
            Content = playQuizControl
        };


        return new[] { createWindow, editWindow, playWindow };
    }

    public void ShowWindow(string windowName)
    {
        ControlWindows.TryGetValue(windowName, out var window);
        if (window == null) return;

        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.ShowDialog();
    }

    public static void StartQuiz(Quiz selectedQuiz)
    {
        UserControl inGameControl = new InGameControl(selectedQuiz);

        var inGameWindow = new CustomWindow
        {
            Title = "Playing",
            Name = "InGame",
            Content = inGameControl
        };

        inGameWindow.Show();
    }
}