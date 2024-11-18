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
        public Guid Id{ get; set; } = Guid.NewGuid();
        public DateTime OrderDate{ get; set; }
        public decimal TotalAmount{ get; set; }
        public string CustomerId{ get; set; } = default!;
        public string ProductId{ get; set; } = default!;
        public OrderStatus OrderStatus { get; set; } = default!;
        public Customer Customer{ get; set; } = default!;
        public ICollection<OrderProduct> OrderProducts{ get; set; } = new HashSet<OrderProduct>();
        public int Quantity{ get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }


    public class OrderRequestModel
    {
        public int Quantity{ get; set; }
        public Guid CustomerId {get; set; }
        public Guid UserId {get; set; }
        public ICollection<OrderItem> OrderItems {get; set;} = new HashSet<OrderItem>();
    }
}