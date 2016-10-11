using SQLite.Net.Interop;

namespace CatalogApp
{
	public interface IPlatformDependency
	{
		ISQLitePlatform GetPlatform();
		string GetDataBasePath();
	}
}