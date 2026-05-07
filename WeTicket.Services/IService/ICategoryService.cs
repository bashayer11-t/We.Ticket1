using WeTicket.Data.Models; 

namespace WeTicket.Services.IService;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(long id);
    Task<Category> CreateAsync(Category category);
    Task<Category?> UpdateAsync(long id, Category category);
    Task<Category?> DeleteAsync(long id);
}