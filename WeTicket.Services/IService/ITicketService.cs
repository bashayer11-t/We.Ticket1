
using WeTicket.Data.Models;

namespace WeTicket.Services.IService;

public interface ITicketService
{
    Task<IEnumerable<Ticket>> GetAllAsync();
    Task<IEnumerable<Ticket>> GetUserTicketsAsync(long userId); // لجلب حجوزات مستخدم معين
    Task<Ticket?> GetByIdAsync(long id);
    Task<Ticket> CreateAsync(Ticket ticket);
    Task<Ticket?> UpdateAsync(long id, Ticket ticket);
    Task<Ticket?> DeleteAsync(long id);
}
