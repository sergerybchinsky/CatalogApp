using System;
using System.Runtime.Serialization;

namespace CatalogApp
{
	[DataContract]
	public class Subject
	{
		[DataMember(Name = "id")]
		public int ID { get; set; }
		[DataMember(Name = "title")]
		public string Title { get; set; }
	}
}