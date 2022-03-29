using System;
using System.Collections.Generic;
using System.Text;
using VHSStore.Domain.Models.MovieModels;

namespace VHSStore.Application.Interfaces.Repos_Interfaces
{
    public interface IMovieRepository : IGenericRepository<MovieModel>
    {
    }
}
