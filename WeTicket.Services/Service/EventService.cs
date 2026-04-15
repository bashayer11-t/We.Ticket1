using Microsoft.EntityFrameworkCore;
using WeTicket.Data.Data;
using WeTicket.Data.Models;
using WeTicket.Services.IService;

namespace WeTicket.Services.Service;

public class EventService(AppDbContext context) : IEventService
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        // استخدام Include إذا كنتِ تريدين جلب بيانات الـ Category المرتبطة بالفعالية
        return await _context.Event.Include(e => e.Category).ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(long id)
    {
        return await _context.Event.Include(e => e.Category)
                                   .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Event> CreateAsync(Event @event)
    {
        await _context.Event.AddAsync(@event);
        await _context.SaveChangesAsync();
        return @event;
    }

    public async Task<Event?> UpdateAsync(long id, Event @event)
    {
        var existingEvent = await _context.Event.FindAsync(id);

        if (existingEvent == null) return null;

        // تحديث البيانات (تأكدي من مطابقة الأسماء للمودل الخاص بكِ)
        existingEvent.Name = @event.Name;
        existingEvent.Description = @event.Description;
        existingEvent.StartDate = @event.StartDate;
        existingEvent.EndDate = @event.EndDate;
        existingEvent.Location = @event.Location;
        existingEvent.CategoryId = @event.CategoryId; // ربطها بالتصنيف الجديد إذا تغير

        await _context.SaveChangesAsync();
        return existingEvent;
    }

    public async Task<Event?> DeleteAsync(long id)
    {
        var @event = await _context.Event.FindAsync(id);

        if (@event == null) return null;

        _context.Event.Remove(@event);
        await _context.SaveChangesAsync();
        return @event;
    }
}
