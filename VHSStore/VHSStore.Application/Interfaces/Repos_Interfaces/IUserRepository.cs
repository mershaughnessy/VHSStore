using System.Collections.Generic;
using System.Threading.Tasks;
using VHSStore.Domain.Models;

namespace VHSStore.Application.Interfaces
{
    public interface IUserRepository
    {
        public Task<int> AddAsync(User entity);
        public Task<int> DeleteAsync(string id);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> GetByIdAsync(string id);
        public Task<User> GetByUserNameAsync(string userName);
    }
}
