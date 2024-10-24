using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Enums;

namespace WardrobeOrganizerApp.Dtos
{
    public class OrderResponseModel
    {
        public Guid Id{ get; set; } 
        public DateTime DateTime{ get; set; }
        public decimal TotalPrice{ get; set; }
        public string CustomerId{ get; set; } = default!;
        public string ProductId{ get; set; } = default!;
        public Status Status { get; set; }
        public Customer Customer{ get; set; } = default!;
        public ICollection<OrderProduct> OrderProducts{ get; set; } = new HashSet<OrderProduct>();
        public int Quantity{ get; set; }
        public bool IsApproved {get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }


    public class OrderRequestModel
    {
        public int Quantity{ get; set; }
        public Guid Id{ get; set; } 

    }
}