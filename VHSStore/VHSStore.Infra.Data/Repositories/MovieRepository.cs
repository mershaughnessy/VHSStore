using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces.Repos_Interfaces;
using VHSStore.Domain.Models.MovieModels;
using VHSStore.Utility.Sql;

namespace VHSStore.Infra.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DapperWrap _dapperWrap;

        public MovieRepository(IConfiguration configuration)
        {
            _dapperWrap = new DapperWrap(configuration.GetConnectionString("VHSStoreDBConnection"));
        }

        public async Task<int> AddAsync(MovieModel entity)
        {
            var result = await _dapperWrap.ExecuteAsync(@"INSERT INTO [Movies]
                        (IndexId, MovieName, GenreId, Description, StockNumber, StockPrice, Year) VALUES
                        (newId(), @MovieName, @GenreId, @Description, @StockNumber, @StockPrice, @Year)", entity);
            return result;
        }

        public async Task<int> DeleteAsync(string id)
        {
            var result = await _dapperWrap.ExecuteAsync(@"DELETE FROM [Movies] WHERE [IndexId] = @IndexId", new { IndexId = id });
            return result;
        }

        public async Task<IEnumerable<MovieModel>> GetAllAsync()
        {
            var result = await _dapperWrap.QueryAsync<MovieModel>(@"SELECT * FROM [Movies]");
            return result;
        }

        public async Task<MovieModel> GetByIdAsync(string id)
        {
            var result = await _dapperWrap.QuerySingleAsync<MovieModel>(@"SELECT * FROM [Movies] WHERE IndexId = @IndexId", new { IndexId = id });
            return result;
        }

        public async Task<int> UpdateAsync(MovieModel entity)
        {
            var result = await _dapperWrap.ExecuteAsync(@"UPDATE [Movies] SET [MovieName] = @MovieName, [GenreId] = @GenreId, [Description] = @Description, 
                    [StockNumber] = @StockNumber, [StockPrice] = @StockPrice, [Year] = @Year", entity);
            return result;
        }

        public async Task<int> StockChangeAsync(string movieId, int stockChange)
        {
            var result = await _dapperWrap.ExecuteAsync(
                @"UPDATE [Movies] SET [StockNumber] = [StockNumber] + @StockChange WHERE [IndexId] = @IndexId",
            new
            {
                StockChange = stockChange,
                IndexId = movieId
            });
            return result;
        }
    }
}
