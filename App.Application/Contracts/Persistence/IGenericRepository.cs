using System.Linq.Expressions;

namespace App.Application.Contracts.Persistence
{
    public interface IGenericRepository<T, TId> where T : class where TId : struct
    {
        public Task<bool> AnyAsync(TId id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllAsync();//IQueryable<T> GetAllAsync() would be better. developer can use orderby after getting the result.
        Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);// IQueryable provides performance because what is implemented before toList(),
                                                                 // for example orderby is executed in db not memory
        ValueTask<T?> GetByIdAsync(int id);
        ValueTask AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);// there is no async for update and delete in ef code. Only the state is changed, these processes don't take long.
    }
}