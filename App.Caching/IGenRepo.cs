using System;
using System.Collections.Generic;
using System.Text;

namespace App.Caching
{
    public interface IGenRepo<T> where T : class
    {
        Task Create(T obj);
        Task<List<T>> GetAll();
        ValueTask<T?> GetByIdAsync(int id);
        void Update(T obj);
        void Delete(T obj);
    }
}
