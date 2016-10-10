using System;
using System.Runtime.Serialization;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace CatalogApp
{
	[DataContract]
	public class Category
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		[DataMember(Name="title")]
		public string Title { get; set; }

		[OneToMany(CascadeOperations = CascadeOperation.All)]      // One to many relationship with Valuation
		[DataMember(Name = "subs")]
		public Subject[] Subjects { get; set; }
	}
}