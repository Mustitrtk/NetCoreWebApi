using App.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductAsync(count);

            var productsAsDto = products.Select(p=> new ProductDto(p.Id,p.Name,p.Price,p.Stock)).ToList(); //Manuel mapper - hızlı çalışır

            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }

        public async Task<ServiceResult<Product>> GetProductByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if(product is null)
            {
                return ServiceResult<Product>.Fail("Product not found", HttpStatusCode.NotFound);
            }

            return ServiceResult<Product>.Success(product);
        }
    }
}
