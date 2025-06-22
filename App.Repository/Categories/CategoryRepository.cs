using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Categories
{
    public class CategoryRepository(AppDbContext context) : GenericRepository<Category>(context), ICategoryRepository
    {
        public Task<Category?> GetCategoryWithProductAsync(int id)
        {
            return context.Categories.Include(p=>p.Products).FirstOrDefaultAsync(x => x.Id == id);
        }

        public IQueryable<Category?> GetCategoryWithProductAsync()
        {
            return context.Categories.Include(p => p.Products).AsQueryable();
        }
    }
}
