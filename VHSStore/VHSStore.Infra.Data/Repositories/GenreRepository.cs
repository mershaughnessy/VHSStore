using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces;
using VHSStore.Domain.Models.GenreModels;
using VHSStore.Utility.Sql;

namespace VHSStore.Infra.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DapperWrap _dapperWrap;

        public GenreRepository(IConfiguration configuration)
        {
            _dapperWrap = new DapperWrap(configuration.GetConnectionString("VHSStoreDBConnection"));
        }

        public async Task<int> AddAsync(GenreModel entity)
        {
            var result = await _dapperWrap.ExecuteAsync(
                @"INSERT INTO [Genres] (IndexId, GenreName) VALUES (newId(), @GenreName)", entity);
            return result;
        }

        public async Task<int> DeleteAsync(string id)
        {
            var result = await _dapperWrap.ExecuteAsync(@"DELETE FROM [Genres] WHERE [IndexId] = @IndexId", new { IndexId = id });
            return result;
        }

        public async Task<IEnumerable<GenreModel>> GetAllAsync()
        {
            var result = await _dapperWrap.QueryAsync<GenreModel>(@"SELECT * FROM [Genres]");
            return result;
        }

        public async Task<GenreModel> GetByIndexIdAsync(string indexId)
        {
            var result = await _dapperWrap.QuerySingleAsync<GenreModel>(@"SELECT * FROM [Genres] WHERE [IndexId] = @IndexId",
                new { IndexId = indexId });
            return result;
        }

        public async Task<int> UpdateAsync(GenreModel entity)
        {
            var result = await _dapperWrap.ExecuteAsync(@"UPDATE [Genres] SET [GenreName] = @GenreName
                        WHERE [IndexId] = @IndexId", entity);
            return result;
        }
    }
}
