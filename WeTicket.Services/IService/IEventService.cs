using WeTicket.Data.Models;

namespace WeTicket.Services.IService;

public interface IEventService
{
    Task<IEnumerable<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(long id);
    Task<Event> CreateAsync(Event @event); // تم استخدام @ لأن event كلمة محجوزة في C#
    Task<Event?> UpdateAsync(long id, Event @event);
    Task<Event?> DeleteAsync(long id);
}