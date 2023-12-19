using Api202.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api202.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        

        public async Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take)
        {
           ICollection<Category> categories=await _repository.GetAll(skip:(page-1)*take,take:take).ToListAsync();

           ICollection<GetCategoryDto> dtos = new List<GetCategoryDto>();
            foreach (var category in categories)
            {
                dtos.Add(new GetCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                });
            }
            return dtos;
        }
        public async Task<GetCategoryDto> GetAsync(int id)
        {
            Category category= await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("Not found");
            return new GetCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }
        public async Task Create(GetCategoryDto dto)
        {
            await _repository.AddAsync(new Category
            {
                Name = dto.Name 
            });
            await _repository.SaveChangeAsync();
        }
    }
}
