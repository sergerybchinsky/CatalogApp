using System.Threading.Tasks;
using System.Collections.Generic;

namespace CatalogApp
{
	public interface IDataService
	{
		Task UpdateDB(Category[] categories);
		Task FirstInit();
		Task<List<Category>> GetCategories();
		IDataServiceDelegate Delegate { get; set; }
		Task<List<Subject>> GetSubjectsByCategoryId(int categoryId);
		Task<Subject> GetSubjectById(int subjectId);
	}
}