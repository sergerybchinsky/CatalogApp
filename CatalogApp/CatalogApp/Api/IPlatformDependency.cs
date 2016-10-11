using System;

using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CatalogApp
{
	public interface IPlatformDependency
	{
		ISQLitePlatform GetPlatform();
		string GetDataBasePath();

	}
}