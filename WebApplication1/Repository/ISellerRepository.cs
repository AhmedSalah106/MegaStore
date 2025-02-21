using MegaMarket.Models;
using MegaMarket1.Models;

namespace MegaMarket.Repository
{
    public interface ISellerRepository
    {
        Seller GetById(int Id, string Includes = null);
        List<Seller> GetAll(string Includes = null);
        void Insert(Seller seller);
        void Update(int Id);
        void Delete(int Id);
        void Save();
    }
}