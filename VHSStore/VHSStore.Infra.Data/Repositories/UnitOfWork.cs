using System;
using System.Collections.Generic;
using System.Text;
using VHSStore.Application.Interfaces;

namespace VHSStore.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IUserRepository userRepository)
        {
            Users = userRepository;
        }

        public IUserRepository Users { get; }
    }
}
