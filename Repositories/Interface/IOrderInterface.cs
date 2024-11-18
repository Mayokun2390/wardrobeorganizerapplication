using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;
// using Mysqlx.Crud;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface IOrderInterface
    {
         Task<Order> MakeOrder(Order order);
        Task<Order> GetOrderById(Guid id);
        Task<ICollection<Order>> GetAllOrder();
        Order Update(Order order);
        bool Delete(Order order);        
        Task<Order> ApprovedOrder (Guid id);
        Task<Order> NotApprovedOrder (Guid id);
        Task<ICollection<Order>> GetAllApprovedOrder(Expression<Func<Order, bool>> predicate); 
        Task<ICollection<Order>> GetAllDisApprovedOrder(Expression<Func<Order, bool>> predicate); 
    }
}