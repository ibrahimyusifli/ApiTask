using System.Linq.Expressions;

namespace Api202.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context):base(context) 
        {
            
        }
    }
}
