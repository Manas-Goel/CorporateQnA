using Models;
using Services.Helpers;
using Services.Integrate;
using System.Collections.Generic;

namespace Services
{
    public class CategoryService:ICategoryService
    {
        private readonly Database Database;

        public CategoryService()
        {
            Database = new Database("CorporateQnADatabase", "SqlServer");
        }

        public void AddCategory(Category categoryParam)
        {
            Database.Execute(
                "INSERT INTO Categories(Name, Description, QuestionsTagged)" +
                "VALUES(@0,@1,0)",
                categoryParam.Name,categoryParam.Description);
            return;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return Database.Query<Db.Category>("SELECT * FROM Categories ORDER BY QuestionsTagged DESC")
                .MapTo<IEnumerable<Category>>();
        }

        public IEnumerable<Category> SearchCategoriesByKeyword(string keyword, int sortBy)
        {
            string orderBy = "";
            if (sortBy == 1)
            {
                orderBy = "QuestionsTagged DESC";
            }

            return Database.Query<Db.Category>($"SELECT * FROM Categories WHERE Name LIKE '%{keyword}%' ORDER BY {orderBy}")
                .MapTo<IEnumerable<Category>>();
        }
    }
}
