using MegaMarket.Models;
using MegaMarket1.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Service;

namespace MegaMarket.Repository
{
    public class SellerRepository:ISellerRepository
    {
        private readonly ApplicationDbContext context;
        public SellerRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public void Delete(int Id)
        {
            Seller seller = GetById(Id);
            context.Remove(seller);
        }
        public List<Seller> GetAll(string Includes = null)
        {
            if (Includes == null)
                return context.Sellers.ToList();
            else
                return context.Sellers.Include(Includes).ToList();
        }

        public Seller GetById(int Id, string Includes = null)
        {
            if (Includes != null)
                return context.Sellers.Include(Includes).FirstOrDefault(e => e.Id == Id);
            else
                return context.Sellers.FirstOrDefault(e => e.Id == Id);
        }

        public void Insert(Seller seller)
        {
            context.Add(seller);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(int Id)
        {
            Seller seller = GetById(Id);
            context.Update(seller);
        }
    }
}
