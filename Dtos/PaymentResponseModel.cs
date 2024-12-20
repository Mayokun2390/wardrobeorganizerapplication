using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Enums;

namespace WardrobeOrganizerApp.Dtos
{
    public class PaymentResponseModel
    {
        public Guid Id{ get; set; } = Guid.NewGuid();
        public decimal Amount{ get; set; }
        public PaymentStatus PaymentStatus{get; set; }
        public DateTime DateCreated{get; set; }
        public Customer Customer {get; set;} = default!;
        public Guid CustomerId {get; set;}
        public Order Order {get; set;} = default!;
        public Guid OrderId {get; set;}
    }


    public class PaymentRequestModel
    {

        public decimal Amount{ get; set; }
    }
}