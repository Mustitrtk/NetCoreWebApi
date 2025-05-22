using App.Repositories;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
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

        //Get All
        public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
        {
            var products = await productRepository.GetAll().ToListAsync();

            var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }

        //Get Paged
        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            var products = await productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();

            #region manuel mapping

            var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            #endregion

            //var productsAsDto = mapper.Map<List<ProductDto>>(products);
            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }

        //Get By Id
        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult<ProductDto?>.Fail("Product not found", HttpStatusCode.NotFound);
            }

            var productDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

            return ServiceResult<ProductDto>.Success(productDto)!;
        }

        //Create
        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest createProduct)
        {
            var product = new Product()
            {
                Name = createProduct.Name,
                Price = createProduct.Price,
                Stock = createProduct.Stock,
            };
            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),$"api/products/{product.Id}");
        }

        //Update
        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest productRequest)
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

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        //Patch
        public async Task<ServiceResult> UpdateProductStockAsync(UpdateProductStockRequest productStockRequest)
        {
            var product = await productRepository.GetByIdAsync(productStockRequest.id);

            if(product is null)
            {
                return ServiceResult.Fail("Product Not Found",HttpStatusCode.NotFound);
            }
            product.Stock = productStockRequest.quantity;

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        //Delete
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if(product is null)
            {
                return ServiceResult.Fail("Product Not Found!",HttpStatusCode.NotFound);
            }

            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
