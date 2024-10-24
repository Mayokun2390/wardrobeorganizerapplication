using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Enums;

namespace WardrobeOrganizerApp.Entities
{
    public class Payment
    {
        public Guid Id{ get; set; } 
        public decimal Amount{ get; set; }
        public PaymentStatus PaymentStatus{get; set; }
        public DateTime DateCreated{get; set; }
        public Customer Customer {get; set;} = default!;
        public Guid CustomerId {get; set;}
        public Order Order {get; set;} = default!;
        public Guid OrderId {get; set;}
        public string PaymentMethod {get; set;} = default!;
        public string PaymentReference {get; set;} = default!;
    }
}