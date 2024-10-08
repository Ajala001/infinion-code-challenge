using App.Domain.Entities;
using System.Linq.Expressions;

namespace App.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        User Update(User user);
        Task<User> GetUserAsync(Expression<Func<User, bool>> predicate);
    }
}
