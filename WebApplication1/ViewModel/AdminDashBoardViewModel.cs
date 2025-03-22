using MegaMarket1.Models;
using System.ComponentModel.DataAnnotations;

namespace MegaStore.ViewModel
{
    public class AdminDashBoardViewModel
    {

        [Range(0, double.MaxValue)]
        public decimal TotalSales {  get; set; }
        [Range(0, int.MaxValue)]
        public int TotalUsers {  get; set; }
        [Range(0, int.MaxValue)]
        public int TotalProducts {  get; set; }
        [Range(0, int.MaxValue)]
        public int TotalOrders {  get; set; }

        public List<Product>? ProductsInMarket { get; set; } = new List<Product>();

    }
}
