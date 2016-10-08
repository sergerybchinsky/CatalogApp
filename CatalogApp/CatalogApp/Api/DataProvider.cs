using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CatalogApp
{
	public class DataProvider
	{
		private static readonly DataProvider _instance = new DataProvider();
		private static readonly string _baseUrl = "https://dl.dropboxusercontent.com/u/50542096/categories-list.json";
		private static readonly string _getMethod = "GET";

		public static DataProvider SharedInstance
		{
			get { return _instance; }
		}



		public void GetData()
		{
			var request = WebRequest.Create(_baseUrl);
			request.Method = _getMethod;
			request.UseDefaultCredentials = true;

			request.BeginGetResponse(new AsyncCallback(GetRequestStreamCallback), request);
		}

		private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
		{ 
			var request = (HttpWebRequest)asynchronousResult.AsyncState;

			using (var response = (HttpWebResponse)request.EndGetResponse(asynchronousResult))
			using (var streamResponse = response.GetResponseStream())
			using (var streamRead = new StreamReader(streamResponse))
			{
				var responseString = streamRead.ReadToEnd();
				GetCatalogFromJson(responseString);
				Debug.WriteLine(responseString);
			}
		}

		private Catalog[] GetCatalogFromJson(string json)
		{ 
			byte[] byteArray = Encoding.Unicode.GetBytes(json);
			using (var stream = new MemoryStream(byteArray))
			{
				var serializer = new DataContractJsonSerializer(typeof(Catalog[]));
				return (Catalog[])serializer.ReadObject(stream);
			}
		}
	}
}

