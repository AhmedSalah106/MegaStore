using MegaStore.Models;

namespace MegaStore.Service
{
    public interface IOrderService
    {
        Order GetById(int Id, string Includes = null);
        List<Order> GetAll(string Includes = null);
        void Insert(Order Order);
        void Update(Order Order);
        void Delete(int Id);
        void Save();
        decimal GetTotalPrice();
        int GetTotalOrders();
    }
}