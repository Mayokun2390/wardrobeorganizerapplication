using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Enums;

namespace WardrobeOrganizerApp.Entities
{
    public class Product
    {
        public Guid Id{ get; set; } 
        public string Name{ get; set; } = default!;
        public int Quantity{get; set; }
        public decimal Price{ get; set; }
        public string ImageUrl{ get; set; } = default!;
        public Category Category{ get; set; } = default!;
        public ICollection<OrderProduct> OrderProducts{get; set; } = new List<OrderProduct>();
        public Cart cart { get; set; } = default!;
        public string CartId{ get; set; } = default!;

    }
}