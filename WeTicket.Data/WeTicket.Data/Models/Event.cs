

namespace WeTicket.Data.Models;
using WeTicket.Data.Models;

public class Event
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public  string Location { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public string EventType { get; set; } = string.Empty;
    public DateTime EndDate { get; set; } 
    public int Capacity { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

}