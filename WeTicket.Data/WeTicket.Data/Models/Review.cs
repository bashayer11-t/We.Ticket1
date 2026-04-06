namespace WeTicket.Data.Models;

public class Review
{
    public long Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;

    // استخدام required يغنيك عن استخدام null! ويحذف التحذير الأصفر
    public required string UserId { get; set; }
    public required ApplicationUser User { get; set; }

    public required long EventId { get; set; }
    public required Event Event { get; set; }
}
