using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VHSStore.Domain.Models.GenreModels;

namespace VHSStore.Application.Interfaces
{
    public interface IGenreRepository : IGenericRepository<GenreModel>
    {
        public Task<GenreModel> GetByIndexIdAsync(string indexId);
    }
}
