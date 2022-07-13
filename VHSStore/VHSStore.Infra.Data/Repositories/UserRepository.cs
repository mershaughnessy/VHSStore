using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using VHSStore.Application.Interfaces;
using VHSStore.Domain.Models;
using VHSStore.Utility.Sql;

namespace VHSStore.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperWrap _dapperWrap;

        public UserRepository(IConfiguration configuration)
        {
            _dapperWrap = new DapperWrap(configuration.GetConnectionString("VHSStoreDBConnection"));
        }

        public async Task<int> AddAsync(User entity)
        {
            var result = await _dapperWrap.ExecuteAsync(
                @"INSERT INTO [Users] (ID, UserName, [Password], Salt, Email, RefreshToken, Subscribed, EmailVerified)
                VALUES (newid(), @UserName, @Password, @Salt, @Email, @RefreshToken, @Subscribed, 0)", entity);
            return result;
        }

        public async Task<int> DeleteAsync(string id)
        {
            var result = await _dapperWrap.ExecuteAsync(@"DELETE FROM [Users] WHERE [ID] = @Id", new { Id = id });
            return result;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var result = await _dapperWrap.QueryAsync<User>(@"SELECT * FROM [Users]");
            return result;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var result = await _dapperWrap.QuerySingleAsync<User>(@"SELECT * FROM [Users] WHERE [Id] = @Id", new { Id = id });
            return result;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var result = await _dapperWrap.QuerySingleAsync<User>(@"SELECT * FROM [Users] WHERE [UserName] = @UserName", new { UserName = userName });
            return result;
        }
    }
}
