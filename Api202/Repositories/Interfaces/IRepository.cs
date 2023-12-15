using System.Linq.Expressions;

namespace Api202.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<IQueryable<Category>> GetAll(Expression<Func<Category, bool>>? expression = null, params string[] includes);
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        void UpdateAsync(Category category);
        void Delete(Category category);
        Task SaveChangeAsync();
        void Update(Category existed);
    }
}
