using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface IPaymentInterface
    {
        Task<Payment> MakePayment(Payment payment);
        Task<Payment> GetById(Guid id);
        Task<ICollection<Payment>> GetAllPayment();
        Task<ICollection<Payment>> GetAllCompletedPayment(Expression<Func<Payment, bool>> predicate);
        Task<ICollection<Payment>> GetAllFailedPayment(Expression<Func<Payment, bool>> predicate);
        Task<ICollection<Payment>> GetAllPendingPayment(Expression<Func<Payment, bool>> predicate);
        Payment Update(Payment payment);
        bool Delete (Payment payment);

    }
}