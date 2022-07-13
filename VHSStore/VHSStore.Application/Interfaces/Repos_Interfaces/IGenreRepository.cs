using System.Collections.Generic;
using System.Threading.Tasks;
using VHSStore.Domain.Models.GenreModels;

namespace VHSStore.Application.Interfaces
{
    public interface IGenreRepository
    {
        public Task<int> AddAsync(GenreModel entity);
        public Task<int> DeleteAsync(string id);
        public Task<IEnumerable<GenreModel>> GetAllAsync();
        public Task<GenreModel> GetByIndexIdAsync(string indexId);
        public Task<int> UpdateAsync(GenreModel entity);
    }
}
