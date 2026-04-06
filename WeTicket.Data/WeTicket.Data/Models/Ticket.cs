namespace WeTicket.Data.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public required string UserId { get; set; }
    public required ApplicationUser User { get; set; }

    public required long EventId { get; set; }
    public required Event Event { get; set; }
}
