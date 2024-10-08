using App.Domain.Interfaces.Repositories;
using App.infrastructure.DataContext;

namespace App.infrastructure.Repositories
{
    public class UnitOfWork(User_ProductDb user_ProductDb) : IUnitOfWork
    {
        public async Task<int> SaveAsync()
        {
            return await user_ProductDb.SaveChangesAsync();
        }
    }
}