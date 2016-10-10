using Foundation;
using UIKit;
using CatalogApp;
using System;
using SQLite.Net.Interop;

namespace CatalogApp.iOS
{

	class PlatformDependency : IPlatformDependency
	{
		public ISQLitePlatform GetPlatform()
		{
			return new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
		}
	}
}
