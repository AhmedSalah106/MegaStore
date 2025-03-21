using MegaStore.Models;

namespace MegaStore.Repository
{
    public interface IOrderRepository
    {
        Order GetById(int Id, string Includes = null);
        List<Order> GetAll(string Includes = null);
        void Insert(Order Order);
        void Update(Order Order);
        void Delete(int Id);
        void Save();
        decimal GetTotalPrice();
    }
}