using System.ComponentModel;
using System.Windows;

namespace Labb3_NET22.DataModels;

public class CustomWindow : Window
{
    public CustomWindow()
    {
        SizeToContent = SizeToContent.WidthAndHeight;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }


    // Hide window on [x] instead of close
    protected override void OnClosing(CancelEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }
}