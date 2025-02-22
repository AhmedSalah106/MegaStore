using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MegaMarket.ViewModel
{
    public class ProductViewModel
    {
        [MinLength(2), MaxLength(50)]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [MinLength(2), MaxLength(50)]
        public string Brand { get; set; }
        public string? imageURL {  get; set; }
        [ForeignKey("Vendor")]
        public int VendorId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [MinLength(2), MaxLength(100)]
        public string Description { get; set; }
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
    }
}
