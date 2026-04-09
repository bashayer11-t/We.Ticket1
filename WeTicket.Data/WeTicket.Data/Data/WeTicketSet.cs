using Microsoft.EntityFrameworkCore;
using WeTicket.Data.Models;

namespace WeTicket.Data.Data;
public partial class AppDbContext
{
    public DbSet<Category>Category { get; set; }
   public DbSet<Event> Event { get; set; }
    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<Review> Review { get; set; }
}