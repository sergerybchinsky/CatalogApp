using System;
using MvvmCross.Platform.Converters;

namespace CatalogApp.iOS.Views
{
	
	public class BusyViewModelToActivitryValueConverter : IMvxValueConverter
	{
		#region IMvxValueConverter implementation
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(bool)value;
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !(bool)value;
		}
		#endregion
	}
}