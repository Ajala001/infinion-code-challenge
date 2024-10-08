using App.Domain.Entities;
using System.Linq.Expressions;

namespace App.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Product Update(Product product);
        void Delete(Product product);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(Expression<Func<Product, bool>> predicate);
    }
}
