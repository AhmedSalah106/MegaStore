using MegaMarket.Models;
using MegaMarket1.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Service;

namespace MegaMarket.Repository
{
    public class VendorRepository:IVendorRepository
    {
        private readonly ApplicationDbContext context;
        public VendorRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public void Delete(int Id)
        {
            Vendor vendor = GetById(Id);
            context.Remove(vendor);
        }

        public List<Vendor> GetAll(string Includes = null)
        {
            if (Includes == null)
                return context.Vendors.ToList();
            else
                return context.Vendors.Include(Includes).ToList();
        }

        public Vendor GetById(int Id, string Includes = null)
        {
            if (Includes != null)
                return context.Vendors.Include(Includes).FirstOrDefault(e => e.Id == Id);
            else
                return context.Vendors.FirstOrDefault(e => e.Id == Id);
        }

        public Vendor GetVendorByName(string Name)
        {
            return context.Vendors.FirstOrDefault(e => e.Name == Name);
        }

        public void Insert(Vendor vendor)
        {
            context.Add(vendor);
        }
        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(int Id)
        {
            Vendor vendor = GetById(Id);
            context.Update(vendor);
        } 
    }
}
