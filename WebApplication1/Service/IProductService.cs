using MegaMarket.ViewModel;
using MegaMarket1.Models;
using WebApplication1.Service;

namespace MegaMarket.Service
{
    public interface IProductService
    {
        Product GetById(int Id, string Includes = null);
        List<Product> GetAll(string Includes = null);
        void Insert(Product product);
        void Update(Product product);
        void Delete(int Id);
        void Save();

        ProductCart GetProductCart(int Id , int Quantity);
        Product GetUpdated(int id, ProductViewModel productVM );
        ProductViewModel GetProductViewModel(int Id);
        List<ProductViewModel> GetAllProductViewModel();
        Product GetProduct(ProductViewModel model);
    }
}