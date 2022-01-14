using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VHSStore.Domain.Models;

namespace VHSStore.Application.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetByUserNameAsync(string userName);
    }
}
