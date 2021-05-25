using Models;
using System.Collections.Generic;

namespace Services.Integrate
{
    public interface ICategoryService
    {
        public void AddCategory(Category category);
        public IEnumerable<Category> GetAllCategories();
        public IEnumerable<Category> SearchCategoriesByKeyword(string keyword, int sortBy);
    }
}
