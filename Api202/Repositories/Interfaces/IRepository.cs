using Api202.Entities.Base;
using System.Linq.Expressions;

namespace Api202.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderExpression = null, bool isDescending = false,bool isTracking=false, params string[] includes);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void Delete(T entity);
        Task SaveChangeAsync();
        void Update(T existed);
    }
}
