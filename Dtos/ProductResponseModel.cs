using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Enums;

namespace WardrobeOrganizerApp.Dtos
{
    public class ProductResponseModel
    {
        public Guid Id{ get; set; } = Guid.NewGuid();
        public string Name{ get; set; } = default!;
        public int Quantity{get; set; }
        public decimal Price{ get; set; }
        public Category Category{ get; set; } = default!;
        public string Picture{ get; set; } = default!;
        public ICollection<OrderProduct> OrderProducts{get; set; } = new HashSet<OrderProduct>();
        public Cart cart { get; set; } = default!;
        public Guid CartId{ get; set; } = default!;
    }


    public class ProductRequestModel
    {
        public string Name{ get; set; } = default!;
        public int Quantity{get; set; }
        public decimal Price{ get; set; }
        public Category Category{ get; set; } = default!;
        public IFormFile Picture{ get; set; } = default!;

    }
}