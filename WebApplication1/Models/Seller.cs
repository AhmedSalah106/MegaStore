using MegaMarket1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Service;

namespace MegaMarket.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [MinLength(3), MaxLength(100)]
        public string Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        [ForeignKey("User")]
        public string UserId {  get; set; }
        public ApplicationUser User { get; set; }

        public string? PhoneNumber { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }
        public List<Product>? Products { get; set; } = new List<Product>();
    }
}
