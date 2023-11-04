using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Labb3_NET22.Helpers;

public class ImageBindConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return FileHandler.ConvertBytesToBmp((byte[])value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return FileHandler.ConvertBmpToBytes((BitmapImage)value);
    }
}