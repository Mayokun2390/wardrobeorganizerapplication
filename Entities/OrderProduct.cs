using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WardrobeOrganizerApp.Entities
{
    public class OrderProduct
    {
        public Guid Id{ get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set;} 
        public Order Order { get; set; } = default!;
        public Guid ProductId { get; set; } 
        public Product Product{ get; set; } = default!;
    }
}