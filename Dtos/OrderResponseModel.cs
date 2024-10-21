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
        public string CustomerId{ get; set; }
        public string ProductId{ get; set; }
        public Status Status { get; set; }
        public Customer Customer{ get; set; }
        public ICollection<OrderProduct> OrderProducts{ get; set; } = new List<OrderProduct>();
        public int Quantity{ get; set; }
        public bool IsApproved {get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }


    public class OrderRequestModel
    {
        public int Quantity{ get; set; }
        public Guid Id{ get; set; } 

    }
}