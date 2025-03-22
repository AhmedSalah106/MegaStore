using MegaStore.Models;
using MegaStore.Repository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MegaStore.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public void Delete(int Id)
        {
            orderRepository.Delete(Id);
        }

        public List<Order> GetAll(string Includes = null)
        {
            return orderRepository.GetAll(Includes);
        }

        public Order GetById(int Id, string Includes = null)
        {
            return orderRepository.GetById(Id, Includes);
        }

        public int GetTotalOrders()
        {
            return orderRepository.GetTotalOrders();
        }

        public decimal GetTotalPrice()
        {
            return orderRepository.GetTotalPrice();
        }

        public void Insert(Order Order)
        {
            orderRepository.Insert(Order);
        }

        public void Save()
        {
            orderRepository.Save();
        }

        public void Update(Order Order)
        {
            orderRepository.Update(Order);
        }
    }
}
