using App.Repositories;
using App.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
    {
        //Get Top
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductAsync(count);

            var productsAsDto = products.Select(p=> new ProductDto(p.Id,p.Name,p.Price,p.Stock)).ToList(); //Manuel mapper - hızlı çalışır

            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }

        //Get By Id
        public async Task<ServiceResult<ProductDto>> GetProductByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult<ProductDto>.Fail("Product not found", HttpStatusCode.NotFound);
            }

            var productDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

            return ServiceResult<ProductDto>.Success(productDto);
        }

        //Create
        public async Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest createProduct)
        {
            var product = new Product()
            {
                Name = createProduct.Name,
                Price = createProduct.Price,
                Stock = createProduct.Stock,
            };
            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id));
        }

        //Update
        public async Task<ServiceResult> UpdateProductAsync(int id, UpdateProductRequest productRequest)
        {
            var product = await productRepository.GetByIdAsync(id);

            if(product is null)
            {
                return ServiceResult.Fail("Data Not Found !",HttpStatusCode.NotFound);
            }

            product.Name = productRequest.Name;
            product.Price = productRequest.Price;
            product.Stock = productRequest.Stock;
            
            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success();
        }

        //Delete
        public async Task<ServiceResult> DeleteProductAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if(product is null)
            {
                return ServiceResult.Fail("Product Not Found!",HttpStatusCode.NotFound);
            }

            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success();
        }
    }
}
