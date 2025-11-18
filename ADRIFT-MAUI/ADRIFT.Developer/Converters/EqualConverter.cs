using System.Globalization;

namespace ADRIFT.Converters;

/// <summary>
/// Converts two values to a boolean indicating whether they are equal
/// Used for showing/hiding tab content based on the current tab
/// </summary>
public class EqualConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        return value.ToString()?.Equals(parameter.ToString(), StringComparison.OrdinalIgnoreCase) ?? false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
