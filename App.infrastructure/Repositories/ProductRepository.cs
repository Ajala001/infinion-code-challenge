using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.infrastructure.Repositories
{
    internal class ProductRepository(User_ProductDb user_ProductDb) : IProductRepository
    {
        public async Task<Product> CreateAsync(Product product)
        {
            await user_ProductDb.Products.AddAsync(product);   
            return product; 
        }

        public void Delete(Product product)
        {
            user_ProductDb.Products.Remove(product);
        }

        public async Task<Product> GetProductAsync(Expression<Func<Product, bool>> predicate)
        {
            var result = await user_ProductDb.Products.FirstOrDefaultAsync(predicate);
            return result;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await user_ProductDb.Products.ToListAsync();
        }

        public Product Update(Product product)
        {
            user_ProductDb.Products.Update(product);
            return product;
        }
    }
}
