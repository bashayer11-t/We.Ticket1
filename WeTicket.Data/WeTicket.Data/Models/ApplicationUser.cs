using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WeTicket.Data.DTOs;

namespace WeTicket.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        public List<RefreshToken>? RefreshTokens { get; set; }
        //public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        //public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}