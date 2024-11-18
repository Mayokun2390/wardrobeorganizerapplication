using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Enums;

namespace WardrobeOrganizerApp.Dtos
{
    public class ProductItemDto
    {
        public Guid Id { get; set; }
        public string Name{ get; set; } = default!;
        public int Quantity{get; set; }
        public decimal Price{ get; set; }
        public string Picture{ get; set; } = default!;
        public Category Category{ get; set; } = default!;
        public Guid CartId{ get; set; } 
        public Cart Cart { get; set; } = default!;


        public class ProductItemDtos
        {
            public ICollection<ProductItemDto> ProductItemDto { get; set; } = new List<ProductItemDto>();
        }
    }


}