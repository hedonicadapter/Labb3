using System;
using System.Globalization;
using System.Windows.Data;

namespace Labb3_NET22.Helpers;

public class ButtonContentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value) return "Finish";
        return "Next";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType == typeof(bool))
        {
            if ((string)value == "Finish") return true;
            return false;
        }

        // Handle other conversion cases if necessary
        return null;
    }
}