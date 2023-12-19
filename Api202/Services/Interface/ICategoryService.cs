namespace Api202.Services.Interface
{
    public interface ICategoryService
    {
        Task<ICollection<GetCategoryDto>> GetAllAsync(int page,int take);
        Task<GetCategoryDto> GetAsync(int id);
        Task Create(GetCategoryDto dto);
    }
}
