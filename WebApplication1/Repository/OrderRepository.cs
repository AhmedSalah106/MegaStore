using MegaStore.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Service;

namespace MegaStore.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly ApplicationDbContext context;
        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Delete(int Id)
        {
            Order order = GetById(Id);
            context.Orders.Remove(order);
        }

        public List<Order> GetAll(string Includes = null)
        {
            if (Includes == null)
                return context.Orders.ToList();
            else
                return context.Orders.Include(Includes).ToList();
        }

        public Order GetById(int Id, string Includes = null)
        {
            if (Includes == null)
                return context.Orders.FirstOrDefault(e=>e.Id==Id);
            else
                return context.Orders.Include(Includes).FirstOrDefault(e=>e.Id==Id);
        }

        public void Insert(Order Order)
        {
            context.Add(Order);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Order Order)
        {
            context.Update(Order);
        }
        public decimal GetTotalPrice()
        {
            return context.Orders.Sum(e => e.TotalAmount);
        }
    }
}
