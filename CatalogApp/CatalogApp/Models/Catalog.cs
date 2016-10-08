using System;
using System.Runtime.Serialization;

namespace CatalogApp
{
	[DataContract]
	public class Catalog
	{
		[DataMember(Name="title")]
		public string Title { get; set; }
		[DataMember(Name = "subs")]
		public Subject[] Subjects { get; set; }
	}
}