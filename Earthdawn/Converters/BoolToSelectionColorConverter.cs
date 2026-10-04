using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Earthdawn.Converters;

public class BoolToSelectionColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool isSelected && isSelected)
        {
            // Return light blue color for selected skills
            return new SolidColorBrush(Colors.LightBlue);
        }
        
        // Return black for unselected skills
        return new SolidColorBrush(Colors.Black);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}