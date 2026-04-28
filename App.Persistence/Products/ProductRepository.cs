using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Products
{
    public class ProductRepository(AppDbContext context) : GenericRepository<Product, int>(context), IProductRepository
    {
        public Task<List<Product>> GetTopPriceProductsAsync(int count)
        {//async and await are not necessary here since we can directly return the Task from ToListAsync,
         //we don't have any additional processing after awaiting the result.
            return Context.Products
                .OrderByDescending(p => p.Price)
                .Take(count).ToListAsync();
        }
    }
}
