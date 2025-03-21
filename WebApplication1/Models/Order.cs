using System.ComponentModel.DataAnnotations;

namespace MegaStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        [MinLength(2),MaxLength(50)]
        public string CustomerName { get; set; }
        [Range(0,double.MaxValue)]
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
