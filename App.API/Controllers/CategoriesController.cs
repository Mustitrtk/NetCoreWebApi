using App.Services.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class CategoriesController(ICategoryService categoryService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories() => CreateActionResult(await categoryService.GetAllListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id) => CreateActionResult(await categoryService.GetByIdAsync(id));

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCategoryWithProducts(int id) => CreateActionResult(await categoryService.GetCategoryWithProductAsync(id));

        [HttpGet("/products")]
        public async Task<IActionResult> GetCategoriesWithProducts() => CreateActionResult(await categoryService.GetCategoryWithProductAsync());

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest createCategoryRequest) => CreateActionResult(await categoryService.CreateAsync(createCategoryRequest));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest updateCategoryRequest) => CreateActionResult(await categoryService.UpdateAsync(id,updateCategoryRequest));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id) => CreateActionResult(await categoryService.DeleteAsync(id));
    }
}
