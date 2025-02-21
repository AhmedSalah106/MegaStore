using MegaMarket1.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Service;

namespace MegaMarket.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;
        public CategoryRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public void Delete(int Id)
        {

            Category category = GetById(Id);
            context.Remove(category);
        }

        public List<Category> GetAll(string Includes = null)
        {
            if (Includes == null)
                return context.Categories.ToList();
            else
                return context.Categories.Include(Includes).ToList();
        }

        public Category GetById(int Id, string Includes = null)
        {
            if (Includes != null)
                return context.Categories.Include(Includes).FirstOrDefault(e => e.Id == Id);
            else
                return context.Categories.FirstOrDefault(e => e.Id == Id);
        }

        public void Insert(Category category)
        {
            context.Add(category);
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public void Update(int Id)
        {
            Category category = GetById(Id);
            context.Update(category);
        }
    }
}
