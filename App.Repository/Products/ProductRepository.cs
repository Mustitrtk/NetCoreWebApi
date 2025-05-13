using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Products
{
    public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
        public Task<List<Product>> GetTopPriceProductAsync(int count) //Async fonksiyon kullanmamamızın sebebi direkt olarak list değer döndürmek.
        {
            return context.Products.OrderByDescending(x=>x.Price).Take(count).ToListAsync();
        }
    }
}
