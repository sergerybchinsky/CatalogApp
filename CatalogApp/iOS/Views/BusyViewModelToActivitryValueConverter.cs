using System;
using CatalogApp.ViewModels;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.Platform.Converters;
using UIKit;

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