using MegaMarket1.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Service;

namespace MegaMarket.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;
        public ProductRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public void Delete(int Id)
        {
            Product product = GetById(Id);
            context.Remove(product);
        }

        public List<Product> GetAll(string Includes = null)
        {
            if(Includes == null)
                return context.Products.ToList();
            else
                return context.Products.Include(Includes).ToList();
        }

        public Product GetById(int Id, string Includes = null)
        {
            if (Includes != null)
                return context.Products.Include(Includes).FirstOrDefault(e => e.Id == Id);
            else
                return context.Products.FirstOrDefault(e => e.Id == Id);
        }

        public void Insert(Product product)
        {
            context.Add(product);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Product product)
        {
            context.Update(product);
        }
    }
}
