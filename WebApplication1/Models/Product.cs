using MegaMarket.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaMarket1.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MinLength(2),MaxLength(50)]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [MinLength(2), MaxLength(50)]
        public string Brand { get; set; }
        [ForeignKey("Vendor")]
        public int VendorId {  get; set; }
        public string? ImageURL {  get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [MinLength(2), MaxLength(100)]
        public string Description { get; set; }
        [Range(0, int.MaxValue)]
        public int Amount {  get; set; }
        public Category? Category { get; set; }
        public Vendor? Vendor { get; set; }

    }
}
