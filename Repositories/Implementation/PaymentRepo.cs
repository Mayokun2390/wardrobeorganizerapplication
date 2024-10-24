using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WardrobeOrganizerApp.Context;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Repositories.Interface;

namespace WardrobeOrganizerApp.Repositories.Implementation
{
    public class PaymentRepo : IPaymentInterface
    {
        private readonly StoreContext _context;
        public PaymentRepo(StoreContext context)
        {
            _context = context;
        }
        public bool Delete(Payment payment)
        {
            _context.Payments.Remove(payment);
            return true;
        }

        public async Task<ICollection<Payment>> GetAllCompletedPayment(Expression<Func<Payment, bool>> predicate)
        {
           var paym = await _context.Payments.Where(predicate).ToListAsync();
           return paym;
        }

        public async Task<ICollection<Payment>> GetAllFailedPayment(Expression<Func<Payment, bool>> predicate)
        {
            var pay = await _context.Payments.Where(predicate).ToListAsync();
           return pay;
        }

        public async Task<ICollection<Payment>> GetAllPayment()
        {
            var payment = await _context.Payments.ToListAsync();
            return payment;
        }

        public async Task<ICollection<Payment>> GetAllPendingPayment(Expression<Func<Payment, bool>> predicate)
        {
            var paym = await _context.Payments.Where(predicate).ToListAsync();
           return paym;
        }

        public async Task<Payment> GetById(Guid id)
        {
            var pay = await _context.Payments.FirstOrDefaultAsync(r => r.Id == id);
            return pay;
        }

        public async Task<Payment> MakePayment(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
           return payment;
        }

        public Payment Update(Payment payment)
        {
            _context.Payments.Update(payment);
            return payment;
        }
    }
}