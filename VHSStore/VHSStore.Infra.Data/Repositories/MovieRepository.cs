using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces.Repos_Interfaces;
using VHSStore.Domain.Models.MovieModels;

namespace VHSStore.Infra.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IConfiguration _configuration;

        public MovieRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> AddAsync(MovieModel entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(
                    @"INSERT INTO [Movies]
                        (IndexId, MovieName, GenreId, Description, StockNumber, StockPrice, Year)
                    VALUES
                        (newId(), @MovieName, @GenreId, @Description, @StockNumber, @StockPrice, @Year)",
                    entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(string id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(
                    @"DELETE FROM [Movies] WHERE [IndexId] = @IndexId",
                    new { IndexId = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<MovieModel>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<MovieModel>(
                    @"SELECT * FROM [Movies]");
                return result.AsList();
            }
        }

        public async Task<MovieModel> GetByIdAsync(string id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleAsync<MovieModel>(
                    @"SELECT * FROM [Movies]");
                return result;
            }
        }

        public async Task<int> UpdateAsync(MovieModel entity)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(
                    @"UPDATE [Movies] SET [MovieName] = @MovieName, [GenreId] = @GenreId, [Description] = @Description, 
                    [StockNumber] = @StockNumber, [StockPrice] = @StockPrice, [Year] = @Year",
                    entity);
                return result;
            }
        }

        public async Task<int> StockChangeAsync(string movieId, int stockChange)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("VHSStoreDBConnection")))
            {
                await connection.OpenAsync();
                var result = await connection.ExecuteAsync(
                    @"UPDATE [Movies] SET [StockNumber] = [StockNumber] + @StockChange
                    WHERE [IndexId] = @IndexId",
                    new
                    { 
                        StockChange = stockChange,
                        IndexId = movieId
                    });
                return result;
            }
        }
    }
}
