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
	[Register("LoadDataView")]
	public class LoadDataView : MvxViewController
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Title = "Welcome";
			this.View.BackgroundColor = UIColor.DarkGray;

			var activityView = new UIActivityIndicatorView();
			activityView.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White;
			activityView.Frame = new CGRect(CGPoint.Empty, new CGSize(100, 100));
			activityView.Center = this.View.Center;
			activityView.StartAnimating();
			activityView.Hidden = true;
			this.View.AddSubview(activityView);

			var processTitle = new UILabel();
			processTitle.Frame = new CGRect(0, activityView.Frame.Y - 50, this.View.Frame.Width, 50);
			processTitle.Font = UIFont.SystemFontOfSize(18);
			processTitle.TextColor = UIColor.White;
			processTitle.TextAlignment = UITextAlignment.Center;
			this.View.AddSubview(processTitle);

			var okButton = new UIButton();
			okButton.Frame = new CGRect(this.View.Frame.Width * 0.25, processTitle.Frame.GetMaxY() + 20,
										processTitle.Frame.Width / 2, processTitle.Frame.Height);
			okButton.SetTitle("Ok!", UIControlState.Normal);
			okButton.BackgroundColor = UIColor.Gray;
			okButton.Layer.BorderWidth = 2;
			okButton.Layer.BorderColor = UIColor.LightGray.CGColor;
			okButton.SetTitleColor(UIColor.White, UIControlState.Normal);
			this.View.AddSubview(okButton);

			var set = this.CreateBindingSet<LoadDataView, LoadDataViewModel>();
			set.Bind(processTitle).For(pt => pt.Text).To(vm => vm.BusyText);
			set.Bind(activityView).For(av => av.Hidden).To(vm => vm.IsBusy).WithConversion(new BusyViewModelToActivitryValueConverter());
			set.Bind(okButton).For(ob => ob.Hidden).To(vm => vm.IsBusy);
			set.Bind(okButton).For(ob => ob).To(vm => vm.ShowCategoriesCommand);
			set.Apply();
		}
	}
}