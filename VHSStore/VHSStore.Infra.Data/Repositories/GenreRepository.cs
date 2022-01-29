using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VHSStore.Application.Interfaces;
using VHSStore.Domain.Models.GenreModels;

namespace VHSStore.Infra.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        public Task<int> AddAsync(GenreModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<GenreModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GenreModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GenreModel> GetByIndexIdAsync(string indexId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(GenreModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
