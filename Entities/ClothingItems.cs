using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Enums;

namespace WardrobeOrganizerApp.Entities
{
    public class ClothingItems
    {
        public Guid Id{ get; set; } 
        public string Name{ get; set; }
        public Category Category { get; set; }
        public string ImageUrl{ get; set; } = default!;
        public decimal Price{ get; set; }
        public string Season{ get; set; }
        public string Brand{ get; set; }
        public Customer Customer { get; set; }
        public string CustomerId{ get; set; } = default!;       
    }
}