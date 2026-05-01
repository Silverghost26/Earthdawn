using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace EarthDawn.Services;

public class StringToImageconverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string path && !string.IsNullOrEmpty(path))
        {
            try
            {
                var uri = new Uri(path);
                var assets = AssetLoader.Open(uri);
                return new Bitmap(assets);
            }
            catch
            {
                throw new Exception("Image not found");
            }
            return null;
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}