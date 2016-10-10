﻿using Foundation;
using UIKit;
using CatalogApp;
using System;
using SQLite.Net.Interop;
using MvvmCross.Platform;
using MvvmCross.iOS.Platform;
using MvvmCross.Core.ViewModels;

namespace CatalogApp.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : MvxApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			var setup = new Setup(this, Window);
			setup.Initialize();

			var startup = Mvx.Resolve<IMvxAppStart>();
			startup.Start();

			Window.MakeKeyAndVisible();

			return true;
		}
	}
}


