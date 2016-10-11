using Foundation;
using UIKit;
using CatalogApp;
using System;
using SQLite.Net.Interop;
using System.IO;

namespace CatalogApp.iOS
{

	class PlatformDependency : IPlatformDependency
	{
		public ISQLitePlatform GetPlatform()
		{
			return new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
		}

		public string GetDataBasePath()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		}
	}
}
