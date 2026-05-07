using WeTicket.Data.Enum;

namespace WeTicket.Data.Models;

public class Category
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public StatusEnum Status { get; set; }
}