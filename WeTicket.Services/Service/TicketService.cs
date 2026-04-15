using Microsoft.EntityFrameworkCore;
using WeTicket.Services.IService;
using WeTicket.Data.Data;
using WeTicket.Data.Models;

namespace WeTicket.Services.Service;

public class TicketService(AppDbContext context) : ITicketService
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        // جلب التذاكر مع تفاصيل الفعالية والمستخدم
        return await _context.Ticket
            .Include(t => t.Event)
            .Include(t => t.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetUserTicketsAsync(long userId)
    {
        return await _context.Ticket
            .Where(t => t.Id == userId)
            .Include(t => t.Event)
            .ToListAsync();
    }

    public async Task<Ticket?> GetByIdAsync(long id)
    {
        return await _context.Ticket
            .Include(t => t.Event)
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Ticket> CreateAsync(Ticket ticket)
    {
        

        await _context.Ticket.AddAsync(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

    public async Task<Ticket?> UpdateAsync(long id, Ticket ticket)
    {
        var existingTicket = await _context.Ticket.FindAsync(id);
        if (existingTicket == null) return null;

       

        await _context.SaveChangesAsync();
        return existingTicket;
    }

    public async Task<Ticket?> DeleteAsync(long id)
    {
        var ticket = await _context.Ticket.FindAsync(id);
        if (ticket == null) return null;

        _context.Ticket.Remove(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }
}