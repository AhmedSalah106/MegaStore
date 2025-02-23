using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MegaMarket.ViewModel;

namespace MegaStore.ViewModel
{
    public class VendorProductsViewModel
    {

        public decimal TotalSailed {  get; set; }
        public List<ProductViewModel> vendorProducts;
    }
}
