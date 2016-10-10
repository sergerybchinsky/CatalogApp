using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace CatalogApp
{
	public delegate void DownloadedSuccesDelegate(Category[] Catalog);

	public class DataProvider
	{
		private static readonly DataProvider _instance = new DataProvider();
		private static readonly string _baseUrl = "https://dl.dropboxusercontent.com/u/50542096/categories-list.json";
		private static readonly string _getMethod = "GET";

		public DownloadedSuccesDelegate DownloadedSuccesDelegate { get; set; }

		public static DataProvider SharedInstance
		{
			get { return _instance; }
		}

		public async Task<Category[]> GetCategoriesFromSerive()
		{
			var httpClient = new HttpClient();
			var responseBodyAsText = await httpClient.GetStringAsync(_baseUrl);
			var result = GetCategoriesFromJson(responseBodyAsText);
			return result;
		}

		#region Get data with callback method
		public void GetData()
		{
			Debug.WriteLine($"[DataProvider.GetData] : Downloading is start!");

			var request = WebRequest.Create(_baseUrl);
			request.Method = _getMethod;
			request.UseDefaultCredentials = true;
			request.BeginGetResponse(new AsyncCallback(GetRequestStreamCallback), request);
		}

		private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
		{
			Debug.WriteLine($"[DataProvider.GetRequestStreamCallback]!");

			var request = (HttpWebRequest)asynchronousResult.AsyncState;

			using (var response = (HttpWebResponse)request.EndGetResponse(asynchronousResult))
			using (var streamResponse = response.GetResponseStream())
			using (var streamRead = new StreamReader(streamResponse))
			{
				var responseString = streamRead.ReadToEnd();
				var catalog = GetCategoriesFromJson(responseString);
				Debug.WriteLine($"[DataProvider.GetRequestStreamCallBack] : Catalod downloaded!");
				if (DownloadedSuccesDelegate != null)
					DownloadedSuccesDelegate(catalog);
			}
		}

		private Category[] GetCategoriesFromJson(string json)
		{ 
			byte[] byteArray = Encoding.Unicode.GetBytes(json);
			using (var stream = new MemoryStream(byteArray))
			{
				var serializer = new DataContractJsonSerializer(typeof(Category[]));
				return (Category[])serializer.ReadObject(stream);
			}
		}
		#endregion
	}
}

