using MegaMarket1.Models;

namespace MegaMarket.Repository
{
    public interface IProductRepository
    {
        Product GetById(int Id, string Includes = null);
        List<Product> GetAll(string Includes = null);
        void Insert(Product product);
        void Update(Product product);
        void Delete(int Id);
        void Save();

        int GetTotalProducts();

    }
}