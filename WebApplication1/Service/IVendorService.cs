using MegaMarket.Models;
using MegaMarket.ViewModel;
using MegaMarket1.Models;
using MegaStore.ViewModel;

namespace MegaMarket.Service
{
    public interface IVendorService
    {
        Vendor GetById(int Id, string Includes = null);
        List<Vendor> GetAll(string Includes = null);
        void Insert(Vendor vendor);
        void Update(int Id);
        void Delete(int Id);
        void Save();
        VendorProductsViewModel GetVendorProductsVM(int Id);
        decimal GetTotalSailed(int Id);
        List<Product>GetProductsByVendorId(int Id);
        int GetIdByName(string Name);
        List<ProductViewModel> GetAllProductsVM(List<Product> products);
    }
}