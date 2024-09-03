using System.Globalization;
using System.Windows.Data;
using System;

namespace PomoWidget;

public class TimeStateToBooleanConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is null || parameter is null)
			return false;

		return value.Equals(parameter);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is null || parameter is null)
			return Binding.DoNothing;

		if ((bool)value)
			return parameter;

		return Binding.DoNothing;
	}
}

public class IntAndFormatToStringConverter : IMultiValueConverter
{
	public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		if (values[0] is int intValue && values[1] is bool isTimeFormat)
		{
			if (isTimeFormat)
			{
				// Convert the integer to minutes, format it as mm:00
				var timeSpan = TimeSpan.FromMinutes(intValue);
				return $"{(int)timeSpan.TotalMinutes:D2}:{timeSpan.Seconds:D2}";
			}
			else
			{
				// Display as an integer
				return intValue.ToString();
			}
		}

		return values[0]?.ToString();
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		if (value is string strValue && targetTypes.Length == 2)
		{
			// Expecting a mm:ss format string, convert it back to an integer representing minutes
			if (TimeSpan.TryParseExact(strValue, @"mm\:ss", culture, out TimeSpan timeSpan))
			{
				return new object[] { (int)timeSpan.TotalMinutes, true };
			}
			else if (int.TryParse(strValue, out int intValue))
			{
				return new object[] { intValue, false };
			}
		}

		return new object[] { 0, false }; // Default fallback values
	}
}
