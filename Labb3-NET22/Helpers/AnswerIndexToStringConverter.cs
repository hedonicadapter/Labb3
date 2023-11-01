using System;
using System.Globalization;
using System.Windows.Data;

namespace Labb3_NET22.Helpers;

// Chat-GPT + stack overflow
public class AnswerIndexToStringConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 2)
            if (values[0] is int index && values[1] is string[] answersCollection)
                if (index >= 0 && index < answersCollection.Length)
                    return answersCollection[index];

        return "Invalid Answer";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}