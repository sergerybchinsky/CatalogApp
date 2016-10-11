using CatalogApp.ViewModels;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace CatalogApp.iOS.Views
{
	[Register("LoadDataView")]
	public class LoadDataView : BaseLoadDataView
	{
		private UIButton _okButton;
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_okButton = new UIButton();
			_okButton.Frame = new CGRect(this.View.Frame.Width * 0.25, _processTitle.Frame.GetMaxY() + 20,
										_processTitle.Frame.Width / 2, _processTitle.Frame.Height);
			_okButton.SetTitle("Ok!", UIControlState.Normal);
			_okButton.BackgroundColor = UIColor.Gray;
			_okButton.Layer.BorderWidth = 2;
			_okButton.Layer.BorderColor = UIColor.LightGray.CGColor;
			_okButton.SetTitleColor(UIColor.White, UIControlState.Normal);
			this.View.AddSubview(_okButton);

			var set = this.CreateBindingSet<LoadDataView, LoadDataViewModel>();
			SetBinding(set);
		}

		private void SetBinding(MvxFluentBindingDescriptionSet<LoadDataView, LoadDataViewModel> set)
		{
			set.Bind(this).For(pt => pt.Title).To(vm => vm.Title);
			set.Bind(_processTitle).For(pt => pt.Text).To(vm => vm.BusyText);
			set.Bind(_activityView).For(av => av.Hidden).To(vm => vm.IsBusy).WithConversion(new BusyViewModelToActivitryValueConverter());
			set.Bind(_okButton).For(ob => ob.Hidden).To(vm => vm.IsBusy);
			set.Bind(_okButton).For(ob => ob).To(vm => vm.ShowCategoriesCommand);
			set.Apply();
		}
	}
}