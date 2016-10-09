using System;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CatalogApp
{
	public interface IPlatformDependancy
	{
		ISQLitePlatform GetPlatform();
	}
	
}
