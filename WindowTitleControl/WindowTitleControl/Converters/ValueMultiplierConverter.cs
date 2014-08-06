using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Controls.Converters
{
    public class ValueMultiplierConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty MultiplierProperty = DependencyProperty.Register(
            "Multiplier", typeof (double), typeof (ValueMultiplierConverter), new PropertyMetadata());

        public double Multiplier
        {
            get { return (double) GetValue(MultiplierProperty); }
            set { SetValue(MultiplierProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ParseDouble(value) * Multiplier;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ParseDouble(value) / Multiplier;
        }

        private static double ParseDouble(object value)
        {
            double parsedDouble;
            if (!double.TryParse(value.ToString(), out parsedDouble)) throw new ArgumentException("Value must be parsable as double.", "value");

            return parsedDouble;
        }
    }
}
