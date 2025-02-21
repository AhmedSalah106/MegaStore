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
        void Update(int Id);
        void Delete(int Id);
        void Save();
        List<ProductViewModel> GetAllProductViewModel();
        Product GetProduct(ProductViewModel model);
    }
}