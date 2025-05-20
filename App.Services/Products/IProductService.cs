using App.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public interface IProductService
    {
        Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count);

        Task<ServiceResult<List<ProductDto>>> GetAllAsync();

        Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);

        Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest createProduct);

        Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest productRequest);

        Task<ServiceResult> DeleteAsync(int id);
    }
}
