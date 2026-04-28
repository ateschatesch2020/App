using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Persistence
{
    public interface  IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
