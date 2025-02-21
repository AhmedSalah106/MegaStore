using MegaMarket1.Models;

namespace MegaMarket.Repository
{
    public interface ICategoryRepository
    {
        Category GetById(int Id, string Includes = null);
        List<Category> GetAll(string Includes = null);
        void Insert(Category category);
        void Update(int Id);
        void Delete(int Id);
        void Save();
    }
}