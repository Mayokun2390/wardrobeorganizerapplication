using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Enums;

namespace WardrobeOrganizerApp.Dtos
{
    public class ClothingItemResponseModel
    {
        public Guid Id{ get; set; } 
        public string Name{ get; set; } = default!;
        public Category Category { get; set; }
        public string ImageUrl{ get; set; } = default!;
        public decimal Price{ get; set; }
        public string Season{ get; set; } = default!;
        public string Brand{ get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public string CustomerId{ get; set; } = default!;
    }


    public class ClothingItemRequestModel
    {
        public string Name{ get; set; } = default!;
        public string Season{ get; set; } = default!;
        public string Brand{ get; set; } = default!;
        public IFormFile ImageUrl{ get; set; } = default!;
        public decimal Price{ get; set; }
        public Category Category { get; set; }
        public Guid Id{ get; set; } 


    }
}