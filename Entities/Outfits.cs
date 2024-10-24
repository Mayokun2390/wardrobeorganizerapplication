using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WardrobeOrganizerApp.Entities
{
    public class Outfits
    {
        public Guid Id{ get; set; } 
        public DateTime DateCreated{get; set; }
        public string Occasion {get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public string CustomerId{ get; set; } = default!;
        public string ImageUrl{ get; set; } = default!;
    }
}