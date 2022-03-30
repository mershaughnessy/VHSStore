using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VHSStore.Domain.Models.MovieModels;

namespace VHSStore.Application.Interfaces.Repos_Interfaces
{
    public interface IMovieRepository : IGenericRepository<MovieModel>
    {
        public Task<int> StockChangeAsync(string movieId, int stockChange);
    }
}
