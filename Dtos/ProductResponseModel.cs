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
        public Guid Id{ get; set; } 
        public string Name{ get; set; } = default!;
        public int Quantity{get; set; }
        public decimal Price{ get; set; }
        public Category Category{ get; set; } = default!;

        public string ImageUrl{ get; set; } = default!;
        public ICollection<OrderProduct> OrderProducts{get; set; } = new List<OrderProduct>();
        public Cart cart { get; set; }
        public string CartId{ get; set; }
    }


    public class ProductRequestModel
    {
        public string Name{ get; set; } = default!;
        public int Quantity{get; set; }
        public decimal Price{ get; set; }
        public Category Category{ get; set; } = default!;
        public IFormFile ImageUrl{ get; set; } = default!;
        public string CartId{ get; set; }
        public Guid Id{ get; set; } 

    }
}