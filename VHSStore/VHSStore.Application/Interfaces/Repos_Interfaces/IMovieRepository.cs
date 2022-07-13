using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VHSStore.Domain.Models.MovieModels;

namespace VHSStore.Application.Interfaces.Repos_Interfaces
{
    public interface IMovieRepository
    {
        public Task<int> AddAsync(MovieModel entity);
        public Task<int> DeleteAsync(string id);
        public Task<IEnumerable<MovieModel>> GetAllAsync();
        public Task<MovieModel> GetByIdAsync(string id);
        public Task<int> UpdateAsync(MovieModel entity);
        public Task<int> StockChangeAsync(string movieId, int stockChange);
    }
}
