using App.Repositories;
using App.Repositories.Categories;
using App.Repositories.Products;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using App.Services.Products.Create;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) :ICategoryService
    {
        //GetWithProduct
        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductAsync(int id)
        {
            var categories = await categoryRepository.GetCategoryWithProductAsync(id);

            if(categories is null)
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("Category Not Found !",HttpStatusCode.NotFound);
            }

            var categoryAsDto = mapper.Map<CategoryWithProductsDto>(categories);

            return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);
        }

        //GetWithProduct
        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductAsync()
        {
            var category = await categoryRepository.GetCategoryWithProduct().ToListAsync();

            if (category is null)
            {
                return ServiceResult<List<CategoryWithProductsDto>>.Fail("Category Not Found !", HttpStatusCode.NotFound);
            }

            var categoryAsDto = mapper.Map<List<CategoryWithProductsDto>>(category);

            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
        }

        //GetAll
        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = await categoryRepository.GetAll().ToListAsync();
            var categoryAsDto = mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.Success(categoryAsDto);
        }

        //GetById
        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if(category is null)
            {
                return ServiceResult<CategoryDto>.Fail("Category Not Found !", HttpStatusCode.NotFound);
            }

            var categoryAsDto = mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto>.Success(categoryAsDto);
        }

        //Create
        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {
            //async manuel service business check
            var isCategoryNameExist = await categoryRepository.Where(x => x.Name == request.Name).AnyAsync();

            if (isCategoryNameExist)
            {
                return ServiceResult<int>.Fail("Category Name Not Found !",
                    HttpStatusCode.NotFound);
            }

            var newCategory = new Category{ Name = request.Name};

            await categoryRepository.AddAsync(newCategory);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<int>.SuccessAsCreated(newCategory.Id,$"api/categories/{newCategory}");
        }

        //Update
        public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                return ServiceResult.Fail("Data Not Found !", HttpStatusCode.NotFound);
            }

            var isCategoryNameExist = await categoryRepository.Where(x => x.Name == request.Name && x.Id != category.Id).AnyAsync();

            if (isCategoryNameExist)
            {
                return ServiceResult.Fail("Product Name Not Found !",
                    HttpStatusCode.NotFound);
            }

            category = mapper.Map(request, category);

            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        //Delete
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                return ServiceResult.Fail("Category Not Found !", HttpStatusCode.NotFound);
            }

            categoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
