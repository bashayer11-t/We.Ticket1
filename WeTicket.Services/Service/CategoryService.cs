using Microsoft.EntityFrameworkCore;
using  WeTicket.Services.IService;
using WeTicket.Data.Data;
using WeTicket.Data.Models;

namespace WeTicket.Services.Service;

public class CategoryService(AppDbContext context) : ICategoryService
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Category.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(long id)
    {
        return await _context.Category.FindAsync(id);
    }

    public async Task<Category> CreateAsync(Category category)
    {
        await _context.Category.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> UpdateAsync(long id, Category category)
    {
        var existingCategory = await _context.Category.FindAsync(id);

        if (existingCategory == null)
            return null;

        // تعديل الحقول بناءً على خصائص مودل Category لديكِ
        // لنفترض أن الحقل الأساسي هو Name
        existingCategory.Name = category.Name;

        // إذا كان هناك وصف (Description) أو أي حقول أخرى، أضيفيها هنا:
        // existingCategory.Description = category.Description;

        await _context.SaveChangesAsync();
        return existingCategory;
    }

    public async Task<Category?> DeleteAsync(long id)
    {
        var category = await _context.Category.FindAsync(id);

        if (category == null)
            return null;

        _context.Category.Remove(category);
        await _context.SaveChangesAsync();
        return category;
    }
}