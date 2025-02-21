using System.ComponentModel.DataAnnotations;

namespace MegaMarket1.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MinLength(2), MaxLength(50)]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
