using System;
using System.Collections.Generic;
using System.Text;
using VHSStore.Application.Interfaces;

namespace VHSStore.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IUserRepository userRepository,
            IGenreRepository genreRepository)
        {
            Users = userRepository;
            Genres = genreRepository;
        }

        public IUserRepository Users { get; }
        public IGenreRepository Genres { get; }
    }
}
