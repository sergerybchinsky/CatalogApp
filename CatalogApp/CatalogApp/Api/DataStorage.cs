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


	public class DataStorage
	{
		private readonly string _connectionString = "Catalog.db3";

		private SQLiteAsyncConnection GetConnection()
		{
			var platform = SimpleIoc.Default.GetInstance<IPlatformDependancy>();
			var connectionString = new SQLiteConnectionString(_connectionString, false);
			var connectionWithLock = new SQLiteConnectionWithLock(platform.GetPlatform(), connectionString);
			var conn = new SQLiteAsyncConnection(() => connectionWithLock);

			return conn;                         
		}

		public void FirstInit()
		{
			var conn = GetConnection();
			CreateTables(conn);
		}

		public async Task UpdateDB(Task<Catalog[]> catalogT)
		{
			var catalog = await catalogT;
			await GetConnection().InsertOrReplaceAllAsync(catalog).ContinueWith((arg) =>
			{
				Debug.WriteLine("DB updated;");
			});
		}

		private bool CreateTables(SQLiteAsyncConnection connection)
		{
			try
			{
				connection.CreateTableAsync<Subject>().ContinueWith((results) =>
				{
					Debug.WriteLine("Subject table created!");
				});
				connection.CreateTableAsync<Catalog>().ContinueWith((results) =>
				{
					Debug.WriteLine("Catalog table created!");
				});
				return true;
			}
			catch (SQLiteException ex)
			{
				Debug.WriteLine(ex.Message);
				return false;
			}
		}
	}
}
