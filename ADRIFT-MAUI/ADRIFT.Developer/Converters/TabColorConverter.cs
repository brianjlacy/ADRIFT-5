using System.Globalization;

namespace ADRIFT.Converters;

/// <summary>
/// Converts a tab name to a background color based on whether it's the current tab
/// </summary>
public class TabColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string currentTab && parameter is string tabName)
        {
            // Return primary color for active tab, surface color for inactive
            if (currentTab.Equals(tabName, StringComparison.OrdinalIgnoreCase))
            {
                return Application.Current?.Resources["ADRIFTPrimary"] ?? Colors.Blue;
            }
            return Application.Current?.Resources["ADRIFTSurface"] ?? Colors.LightGray;
        }

        return Application.Current?.Resources["ADRIFTSurface"] ?? Colors.LightGray;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
