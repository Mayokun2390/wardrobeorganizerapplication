using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
// using Mysqlx.Crud;
using WardrobeOrganizerApp.Context;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Enums;
using WardrobeOrganizerApp.Repositories.Interface;

namespace WardrobeOrganizerApp.Repositories.Implementation
{
    public class OrderRepo : IOrderInterface
    {
        private readonly StoreContext _context;

        public OrderRepo(StoreContext context)
        {
            _context = context;
        }

        public async Task<Order> ApprovedOrder(Guid id)
        {
            var status = await _context.Orders.Where(x => x.OrderStatus == OrderStatus.IsApproved).FirstOrDefaultAsync(x => x.Id == id);
            return status;
        }

        public bool Delete(Order order)
        {
            _context.Orders.Remove(order);
            return true;
        }

        public async Task<ICollection<Order>> GetAllApprovedOrder(Expression<Func<Order, bool>> predicate)
        {
            var getApprovedOrders = await _context.Orders.Where(predicate).ToListAsync();
            return getApprovedOrders;
        }

        public async Task<ICollection<Order>> GetAllDisApprovedOrder(Expression<Func<Order, bool>> predicate)
        {
            var getDisapprovedOrder = await _context.Orders.Where(predicate).ToListAsync();
            return getDisapprovedOrder;
        }

        public async Task<ICollection<Order>> GetAllOrder()
        {
            var getOrders = await _context.Orders.ToListAsync();
            return getOrders;
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            var getId = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            return getId;
        }

        public async Task<Order> MakeOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            return order;
        }

        public async Task<Order> NotApprovedOrder(Guid id)
        {
            var status = await _context.Orders.Where(x => x.OrderStatus == OrderStatus.Pending).FirstOrDefaultAsync(x => x.Id == id);
            return status;
        }

        public Order Update(Order order)
        {
            _context.Orders.Update(order);
            return order;
        }
    }
}