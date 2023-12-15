

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api202.Repositories.Implementations
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

      

        public async Task <IQueryable<Category>> GetAll(Expression<Func<Category,bool>>? expression=null,params string[] includes)
        {
            IQueryable<Category> query = _context.Categories;

            if(expression != null)
            {
                query = query.Where(expression);
            }

            if(includes is not null)
            {
                for(int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]); 
                }
            }
            return query;
        }

        public async Task<Category> GetByIdAsync(int id) 
        {  
            Category category=await _context.Categories.FirstOrDefaultAsync(c=>c.Id==id);
            return category;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);           
        }

        public async void Delete(Category category)
        {
            _context.Categories.Remove(category);        
        }

        public void UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
