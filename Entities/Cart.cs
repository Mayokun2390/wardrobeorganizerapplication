using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WardrobeOrganizerApp.Entities
{
    public class Cart
    {
        public Guid Id{ get; set; } 
        public string NameOfProduct{ get; set; } = default!;
        public int Quantity{ get; set; }
        public decimal TotalPrice{ get; set; }
        public string ProductId{ get; set; }
        public ICollection<OrderProduct> OrderProducts{get; set; } = new List<OrderProduct>();

    }
}