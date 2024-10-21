using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Enums;

namespace WardrobeOrganizerApp.Entities
{
    public class Order
    {
        public Guid Id{ get; set; } = Guid.NewGuid();
        public DateTime DateTime{ get; set; }
        public decimal TotalPrice{ get; set; }
        public Guid CustomerId{ get; set; }
        public Guid ProductId{ get; set; }
        public Status Status { get; set; }
        public Customer Customer{ get; set; }
        public ICollection<OrderProduct> OrderProducts{ get; set; } = new List<OrderProduct>();
        public int Quantity{ get; set; }
        public bool IsApproved {get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}