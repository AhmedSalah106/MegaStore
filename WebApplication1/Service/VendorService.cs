using MegaMarket.Models;
using MegaMarket.Repository;
using MegaMarket.ViewModel;
using MegaMarket1.Models;
using Microsoft.VisualBasic;

namespace MegaMarket.Service
{
    public class VendorService:IVendorService
    {
        private readonly IVendorRepository vendorRepository;
        public VendorService(IVendorRepository _vendorRepository)
        {
            vendorRepository = _vendorRepository;
        }

        public List<Vendor> GetAll(string Includes = null)
        {
            return vendorRepository.GetAll();
        }

        public Vendor GetById(int Id,string Includes = null)
        {
            return vendorRepository.GetById(Id);
        }
        public void Insert(Vendor vendor)
        {
            vendor.Name.ToLower();
            vendorRepository.Insert(vendor);
        }
        public void Update(int Id)
        {
            vendorRepository.Update(Id);
        }
        public void Delete(int Id)
        {
            vendorRepository.Delete(Id);
        }
        public void Save()
        {
            vendorRepository.Save();
        }
    }

}
