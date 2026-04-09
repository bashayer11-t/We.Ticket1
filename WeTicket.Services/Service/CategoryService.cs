using Microsoft.EntityFrameworkCore;
using WeTicket.Data.Data;
using WeTicket.Data.Models;
using WeTicket.Services.IService;

namespace WeTicket.Services.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Category.ToListAsync();
        }
    }
}