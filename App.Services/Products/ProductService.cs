using App.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        public Task<List<Product>> GetTopPriceProductAsync(int count)
        {
            return productRepository.GetTopPriceProductAsync(count);
        }
    }
}
