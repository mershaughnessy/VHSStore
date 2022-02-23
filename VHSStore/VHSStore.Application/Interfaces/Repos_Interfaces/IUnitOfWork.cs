using System;
using System.Collections.Generic;
using System.Text;

namespace VHSStore.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IGenreRepository Genres { get; }
    }
}
