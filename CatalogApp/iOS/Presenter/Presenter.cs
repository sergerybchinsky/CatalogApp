using Foundation;
using UIKit;
using CatalogApp;
using System;
using SQLite.Net.Interop;
using MvvmCross.Platform;
using MvvmCross.iOS.Platform;
using MvvmCross.Core.ViewModels;
using CatalogApp.iOS.Views;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.iOS.Views;

namespace CatalogApp.iOS
{
	class Presenter : MvxModalNavSupportIosViewPresenter
	{
		private UINavigationController _mainController = null;

		public Presenter(IMvxApplicationDelegate appDelegate, UIWindow window) : base(appDelegate, window)
		{
		}

		protected override void ShowFirstView(UIViewController viewController)
		{
			CreateNavigationController(viewController);
			this.Window.RootViewController = _mainController;
			_mainController.PushViewController(viewController, true);
		}

		public override void Show(IMvxIosView view)
		{
			if (ShowMainView(view))
				return;

			base.Show(view);
		}
		private bool ShowMainView(IMvxIosView view)
		{
			if (view is CategoriesView)
			{
				_mainController.SetViewControllers(new UIViewController[] { view as UIViewController }, true);
				return true;
			}
			return false;
		}

		protected override UINavigationController CreateNavigationController(UIViewController viewController)
		{
			if (_mainController == null)
				_mainController = new UINavigationController();
			return _mainController;
		}
	}

}
