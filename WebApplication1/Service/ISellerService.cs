using MegaMarket.Models;

namespace MegaMarket.Service
{
    public interface ISellerService
    {
        Seller GetById(int Id, string Includes = null);
        List<Seller> GetAll(string Includes = null);
        void Insert(Seller vendor);
        void Update(int Id);
        void Delete(int Id);
        void Save();
        int GetIdByName(string Name);
    }
}