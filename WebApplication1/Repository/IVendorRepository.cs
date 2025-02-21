using MegaMarket.Models;
using MegaMarket1.Models;

namespace MegaMarket.Repository
{
    public interface IVendorRepository
    {
        Vendor GetById(int Id, string Includes = null);
        List<Vendor> GetAll(string Includes = null);
        void Insert(Vendor vendor);
        void Update(int Id);
        void Delete(int Id);
        void Save();
        Vendor GetVendorByName(string Name);
    }
}