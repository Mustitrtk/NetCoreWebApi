using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductAsync(int id);
        Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductAsync();
        Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
        Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
        Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
