using WeTicket.Data.Models;

namespace WeTicket.Services.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}