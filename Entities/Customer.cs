using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Enums;

namespace WardrobeOrganizerApp.Entities
{
    public class Customer
    {
        public Guid CustomerId{ get; set; } = Guid.NewGuid();
        public string FirstName{ get; set; } = default!;
        public string LastName{ get; set;} = default!;
        public string PhoneNumber{get; set;} = default!;
        public string Email{ get; set; } = default!;
        public ICollection<Order> Order { get; set; } = new HashSet<Order>();
        public ICollection<Payment> Payment { get; set; } = new HashSet<Payment>();
        // public ChartBot chartBot {get; set;}
        public Guid? ChartBotId {get; set;} 
        public Guid CartId {get; set; }
    }
}