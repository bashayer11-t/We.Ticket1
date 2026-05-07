using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeTicket.Data.Enum;

namespace WeTicket.Data.Models;
public class Event
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public  string Location { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public EventTypeEnum EventType { get; set; }
    public DateTime EndDate { get; set; } 
    public int Capacity { get; set; }

    public long CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    [ForeignKey(nameof(UserId))]
    public ApplicationUser? User { get; set; }

    //public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    //public ICollection<Review> Reviews { get; set; } = new List<Review>();
}