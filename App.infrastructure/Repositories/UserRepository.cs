using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.infrastructure.Repositories
{
    public class UserRepository(User_ProductDb user_ProductDb) : IUserRepository
    {
        public async Task<User> CreateAsync(User user)
        {
            await user_ProductDb.Users.AddAsync(user);
            return user;
        }

        public async Task<User> GetUserAsync(Expression<Func<User, bool>> predicate)
        {
            var result = await user_ProductDb.Users.FirstOrDefaultAsync(predicate);
            return result;
        }

        public User Update(User user)
        {
            user_ProductDb.Users.Update(user);
            return user;
        }
    }
}
