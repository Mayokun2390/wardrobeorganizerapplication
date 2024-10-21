using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Dtos
{
    public class OutfitsResponseModel
    {
        public Guid Id{ get; set; } 
        public DateTime DateCreated{get; set; }
        public string Occasion {get; set; }
        public Customer Customer { get; set; }
        public string CustomerId{ get; set; } = default!;
        public string ImageUrl{ get; set; } = default!;

    }


    public class OutfitsRequestModel
    {
        public string Occasion {get; set; }
        public IFormFile ImageUrl{ get; set; } = default!;
        public Guid Id{ get; set; } 

    }
}