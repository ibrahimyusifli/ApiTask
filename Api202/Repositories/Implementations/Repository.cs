

using Api202.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api202.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext context)
        {
            _context = context;
          
            _table = context.Set<T>();
        }

      

        public IQueryable<T> GetAll(
            Expression<Func<T,bool>>? expression=null,
            Expression<Func<T,object>>? orderExpression=null,
            bool isDescending=false,
            int skip = 0,
            int take = 0,
            bool isTracking = false,
            params string[] includes)
        {
            IQueryable<T> query = _table;

            if(expression != null) query = query.Where(expression);

            if(orderExpression != null)
            {
                if (isDescending) query = query.OrderByDescending(orderExpression);
                else query = query.OrderBy(orderExpression);
            }

            if(skip != 0) query = query.Skip(skip);
            if(take != 0) query = query.Take(take);


            if (includes is not null)
            {
                for(int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]); 
                }
            }
            return isTracking?query : query.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(int id) 
        {  
            T entity=await _table.FirstOrDefaultAsync(e=>e.Id==id);
            return entity;
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);           
        }

        public async void Delete(T entity)
        {
            _table.Remove(entity);        
        }

        public void UpdateAsync(T entity)
        {
            _table.Update(entity);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
