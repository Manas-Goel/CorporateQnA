using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Integrate;
using System.Collections.Generic;

namespace CorporateQnA.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }

        [HttpGet("search")]
        public IEnumerable<Category> SearchCategories(string keyword, int sortBy)
        {
            return _categoryService.SearchCategoriesByKeyword(keyword, sortBy);
        }

        [HttpPost]
        public bool AddCategory(Category category)
        {
            _categoryService.AddCategory(category);
            return true;
        }
    }
}
