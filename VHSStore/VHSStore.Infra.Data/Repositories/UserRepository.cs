using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using VHSStore.Application.Interfaces;
using VHSStore.Domain.Models;
using VHSStore.Domain.Models.UserModels;

namespace VHSStore.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> AddAsync(User entity)
        {
            var sql = @"INSERT INTO [Users] (ID, UserName, [Password], Salt, Email, RefreshToken, Subscribed)
                        VALUES (newid(), @UserName, @Password, @Salt, @Email, @RefreshToken, @Subscribed)";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(string id)
        {
            var sql = @"DELETE FROM [Users] WHERE [ID] = @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id});
                return result;
            }
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var sql = @"SELECT * FROM [Users]";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(sql);
                return result.AsList();
            }
        }

        public Task<User> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var sql = @"SELECT * FROM [Users]
                        WHERE [UserName] = @UserName";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<User>(sql, new { UserName = userName});
                return result;
            }
        }
    }
}
