using App.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Caching
{
    public class GenRepo<T>(AppDbContext context) : IGenRepo<T> where T : class
    {
        private DbContext _dbContext = context;
        protected DbSet<T> dbSet = context.Set<T>();

        public async Task Create(T obj)
        {
            await dbSet.AddAsync(obj);
        }

        public void Update(T obj)
        {
             dbSet.Update(obj);
        }

        public void Delete(T obj)
        {
            dbSet.Remove(obj);
        }

        public Task<List<T>> GetAll()
        {
            return dbSet.ToListAsync();
        }

        public ValueTask<T?> GetByIdAsync(int id)
        {
            return dbSet.FindAsync(id);
        }
    }
}
