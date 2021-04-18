using ProfileBook.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Services.Repository
{
    public interface IUserRepository
    {
        Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new();

        Task<List<T>> GetAllAsync<T>() where T : IEntityBase, new();
    }
}
