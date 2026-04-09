using System.ComponentModel.DataAnnotations.Schema;

namespace WeTicket.Data.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string PayStatus { get; set; } = string.Empty;

    public required string UserId { get; set; }
    public required ApplicationUser User { get; set; }

    public required long EventId { get; set; }
    public required Event Event { get; set; }
}
