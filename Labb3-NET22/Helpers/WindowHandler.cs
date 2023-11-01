using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Labb3_NET22.Helpers;

public class WindowHandler
{
    public Dictionary<string, Window> ControlWindows { get; } = new();

    public WindowUtils()
    {
        Window[] windows = InitWindows();
        
        foreach (Window window in windows)
        {
            ControlWindows.Add(window.Title, window);
        }
    }

    private Window[] InitWindows()
    {
        UserControl createQuizControl = new CreateControl();
        UserControl editQuizControl = new EditControl();
        UserControl playQuizControl = new PlayControl();
        
        Window createWindow = new Window
        {
            Title="Create quiz",
            Content = createQuizControl,
        };
        
        Window editWindow = new Window
        {
            Title="Edit quiz",
            Content = editQuizControl,
        };
        
        Window playWindow = new Window
        {
            Title="Play",
            Content = playQuizControl,
        };

        return [createWindow, editWindow, playWindow];
    }
}