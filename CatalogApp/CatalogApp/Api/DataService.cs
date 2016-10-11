using SQLite.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using SQLite.Net.Async;
using SQLiteNetExtensionsAsync.Extensions;
using System.IO;

namespace CatalogApp
{
	public interface IDataServiceDelegate
	{
		Task DataBaseUdpated();
	}
	public class DataService : IDataService
	{
		private readonly string _dbName = "Catalog.db3";
		private string _dbPath;
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
			_dbPath = Path.Combine(_platformDependency.GetDataBasePath(), _dbName);
		}

		private SQLiteAsyncConnection GetConnection()
		{
			var connectionString = new SQLiteConnectionString(_dbPath, false);
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

		public async Task<List<Subject>> GetSubjectsByCategoryId(int categoryId)
		{
			var connection = GetConnection();
			var table = connection.Table<Subject>().Where(x => x.CategoryID == categoryId);
			return await table.ToListAsync();
		}

		public async Task<Subject> GetSubjectById(int subjectId)
		{
			var connection = GetConnection();
			var table = connection.Table<Subject>().Where(x => x.ID == subjectId);
			return await table.FirstOrDefaultAsync();
		}

		public async Task UpdateDB(Category[] categories)
		{
			try
			{
				await WriteOperations.InsertAllWithChildrenAsync(GetConnection(), categories).ContinueWith(async (arg) =>
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