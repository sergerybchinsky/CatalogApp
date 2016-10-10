using SQLite.Net;
using SQLite.Net.Async;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace CatalogApp
{
	public interface IDataServiceDelegate
	{
		Task DataBaseUdpated();
	}
	public class DataService : IDataService
	{
		private readonly string _dbName = "Catalog.db3";
		private IPlatformDependency _platformDependency;

		public IDataServiceDelegate _delegate;
		public IDataServiceDelegate Delegate
		{
			get { return _delegate; }
			set { _delegate = value;}
		}

		public DataService(IPlatformDependency platformDependency)
		{
			_platformDependency = platformDependency;
		}

		private SQLiteAsyncConnection GetConnection()
		{
			var connectionString = new SQLiteConnectionString(_dbName, false);
			var connectionWithLock = new SQLiteConnectionWithLock(_platformDependency.GetPlatform(), connectionString);
			var conn = new SQLiteAsyncConnection(() => connectionWithLock);

			return conn;                         
		}

		public async Task FirstInit()
		{
			var connection = GetConnection();
			try
			{
				await connection.CreateTableAsync<Subject>().ContinueWith((results) =>
				{
					Debug.WriteLine("[DataStorage.CreateTables]: Subject table created!");
				});
				await connection.CreateTableAsync<Category>().ContinueWith((results) =>
				{
					Debug.WriteLine("[DataStorage.CreateTables]: Category table created!");
				});
			}
			catch (SQLiteException ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		public async Task<List<Category>> GetCategories()
		{
			var connection = GetConnection();
			var table = connection.Table<Category>();
			return await table.ToListAsync();
		}

		public async Task UpdateDB(Category[] categories)
		{
			try
			{
				await GetConnection().InsertOrReplaceAllAsync(categories).ContinueWith(async (arg) =>
				{
					Debug.WriteLine("[DataStorage.UpdateDB]: DB updated!");

					await Delegate.DataBaseUdpated();
				});
			} 
			catch (SQLiteException ex) 
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}