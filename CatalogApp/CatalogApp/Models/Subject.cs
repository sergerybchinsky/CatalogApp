using System.Runtime.Serialization;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace CatalogApp
{
	[DataContract]
	public class Subject
	{
		[PrimaryKey]
		[DataMember(Name = "id")]
		public int ID { get; set; }

		[ForeignKey(typeof(Category))]
		public int CategoryID { get; set; }

		[DataMember(Name = "title")]
		public string Title { get; set; }

		[ManyToOne]
		public Category Categories { get; set; }
	}
}