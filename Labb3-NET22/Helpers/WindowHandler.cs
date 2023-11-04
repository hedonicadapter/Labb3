using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Labb3_NET22.DataModels;
using Labb3_NET22.UserControls;

namespace Labb3_NET22.Helpers;

public class WindowHandler
{
    // Chat-GPT
    private static readonly string? AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

    private static CustomWindow? GetWindow(string windowName)
    {
        var className = $"Labb3_NET22.{windowName}Control, {AssemblyName}";
        var controlClass = Type.GetType(className);

        if (controlClass == null) return null;

        // Chat-GPT
        // Create an instance of the class using Activator.CreateInstance
        var instance = Activator.CreateInstance(controlClass);
        if (instance == null) return null;

        var newWindow = new CustomWindow
        {
            Title = windowName + " quiz",
            Name = windowName,
            Content = instance
        };

        return newWindow;
    }

    public void ShowWindow(string windowName)
    {
        var window = GetWindow(windowName);
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
        inGameWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        inGameWindow.Show();
    }
}