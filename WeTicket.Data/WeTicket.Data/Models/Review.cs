namespace WeTicket.Data.Models;

public class Review
{
    public long Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }

    public required long EventId { get; set; }
    public required Event Event { get; set; }
}
