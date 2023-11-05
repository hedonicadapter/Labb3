using System;
using System.Reflection;
using System.Windows.Controls;
using Labb3_NET22.DataModels;
using Labb3_NET22.UserControls;

namespace Labb3_NET22.Helpers;

public class WindowHandler
{
    // Chat-GPT
    private static readonly string? AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

    private static CustomWindow? GetWindow(string windowName, object? data)
    {
        var className = $"Labb3_NET22.{windowName}Control, {AssemblyName}";
        var controlClass = Type.GetType(className);

        if (controlClass == null) return null;

        // Activator.CreateInstance från Chat-GPT
        var instance = data != null
            ? Activator.CreateInstance(controlClass, data)
            : Activator.CreateInstance(controlClass);
        if (instance == null) return null;

        var newWindow = new CustomWindow
        {
            Title = windowName + " quiz",
            Name = windowName,
            Content = instance
        };

        return newWindow;
    }

    public void ShowWindow(string windowName, object? data = null)
    {
        var window = GetWindow(windowName, data);
        if (window == null) return;

        window.ShowDialog();
    }

    public static void StartQuiz(Quiz selectedQuiz)
    {
        UserControl playControl = new PlayControl(selectedQuiz);

        var playWindow = new CustomWindow
        {
            Title = "Playing",
            Name = "Play",
            Content = playControl
        };

        playWindow.Show();
    }

    public static void StartQuizEditor(Quiz selectedQuiz)
    {
    }
}