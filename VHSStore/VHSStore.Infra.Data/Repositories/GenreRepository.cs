using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces;
using VHSStore.Domain.Models.GenreModels;

namespace VHSStore.Infra.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IConfiguration _configuration;

        public GenreRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> AddAsync(GenreModel entity)
        {
            var sql = @"INSERT INTO [Genres] (IndexId, GenreName)
                        VALUES (newId(), @GenreName)";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(string id)
        {
            var sql = @"DELETE FROM [Genres] WHERE [IndexId] = @IndexId";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(sql, new { IndexId = id});
                return result;
            }
        }

        public async Task<IReadOnlyList<GenreModel>> GetAllAsync()
        {
            var sql = @"SELECT * FROM [Genres]";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<GenreModel>(sql);
                return result.AsList();
            }
        }

        public async Task<GenreModel> GetByIndexIdAsync(string indexId)
        {
            var sql = @"SELECT * FROM [Genres] WHERE [IndexId] = @IndexId";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<GenreModel>(sql, new { IndexId = indexId });
                return result;
            }
        }

        public async Task<int> UpdateAsync(GenreModel entity)
        {
            var sql = @"UPDATE [Genres] SET [GenreName] = @GenreName
                        WHERE [IndexId] = @IndexId";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        Task<GenreModel> IGenericRepository<GenreModel>.GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
