using CoreGraphics;
using MvvmCross.iOS.Views;
using UIKit;

namespace CatalogApp.iOS.Views
{
	public abstract class BaseLoadDataView : MvxViewController
	{
		protected UIActivityIndicatorView _activityView;
		protected UILabel _processTitle;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.View.BackgroundColor = UIColor.DarkGray;

			_activityView = new UIActivityIndicatorView();
			_activityView.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White;
			_activityView.Frame = new CGRect(CGPoint.Empty, new CGSize(100, 100));
			_activityView.Center = this.View.Center;
			_activityView.StartAnimating();
			_activityView.Hidden = true;
			this.View.AddSubview(_activityView);

			_processTitle = new UILabel();
			_processTitle.Frame = new CGRect(0, _activityView.Frame.Y - 50, this.View.Frame.Width, 50);
			_processTitle.Font = UIFont.SystemFontOfSize(18);
			_processTitle.TextColor = UIColor.White;
			_processTitle.TextAlignment = UITextAlignment.Center;
			this.View.AddSubview(_processTitle);
		}
	}
}