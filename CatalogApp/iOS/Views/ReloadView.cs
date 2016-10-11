using CatalogApp.ViewModels;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CatalogApp.iOS.Views
{
	[Register("ReloadView")]
	public class ReloadView : BaseLoadDataView
	{
		private UIButton _okButton;
		private UIButton _realodButton;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_realodButton = new UIButton();
			_realodButton.Frame = new CGRect(this.View.Frame.Width * 0.25, _processTitle.Frame.GetMaxY() + 20,
										_processTitle.Frame.Width / 2, _processTitle.Frame.Height);
			_realodButton.SetTitle("Reload DB", UIControlState.Normal);
			_realodButton.BackgroundColor = UIColor.Gray;
			_realodButton.Layer.BorderWidth = 2;
			_realodButton.Layer.BorderColor = UIColor.LightGray.CGColor;
			_realodButton.SetTitleColor(UIColor.White, UIControlState.Normal);
			_realodButton.TouchUpInside += (sender, e) => _realodButton.Hidden = true;
			_realodButton.Layer.CornerRadius = 5;
			this.View.AddSubview(_realodButton);

			_okButton = new UIButton();
			_okButton.Frame = new CGRect(this.View.Frame.Width * 0.25, _realodButton.Frame.GetMaxY() + 20,
										_processTitle.Frame.Width / 2, _processTitle.Frame.Height);
			_okButton.SetTitle("Ok", UIControlState.Normal);
			_okButton.BackgroundColor = UIColor.Gray;
			_okButton.Layer.BorderWidth = 2;
			_okButton.Layer.CornerRadius = 5;
			_okButton.Layer.BorderColor = UIColor.LightGray.CGColor;
			_okButton.SetTitleColor(UIColor.White, UIControlState.Normal);
			_okButton.TouchUpInside += (sender, e) => this.NavigationController.PopViewController(true);
			this.View.AddSubview(_okButton);

			var set = this.CreateBindingSet<ReloadView, ReloadViewModel>();
			SetBinding(set);
		}

		private void SetBinding(MvxFluentBindingDescriptionSet<ReloadView, ReloadViewModel> set)
		{
			set.Bind(this).For(pt => pt.Title).To(vm => vm.Title);
			set.Bind(_processTitle).For(pt => pt.Text).To(vm => vm.BusyText);
			set.Bind(_activityView).For(av => av.Hidden).To(vm => vm.IsBusy).WithConversion(new BusyViewModelToActivitryValueConverter());
			set.Bind(_realodButton).For(ob => ob.Hidden).To(vm => vm.IsBusy);
			set.Bind(_realodButton).For(ob => ob).To(vm => vm.ReloadDBCommand);
			set.Apply();
		}
	}
}