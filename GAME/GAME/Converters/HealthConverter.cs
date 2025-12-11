using System.Globalization;

namespace GAME.Converters
{
    public class HealthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int health)
            {
                // Assuming max health is 100 for progress bar (0-1 range)
                return Math.Min(1.0, health / 100.0);
            }
            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
