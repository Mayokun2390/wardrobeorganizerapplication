using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Repositories.Interface
{
    public interface IPaymentInterface
    {
        Task<Payment> MakePayment(Payment payment);
        Task<Payment> GetById(Guid id);
        Task<ICollection<Payment>> GetAllPayment();
        Payment Update(Payment payment);
        bool Delete (Payment payment);

    }
}