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
