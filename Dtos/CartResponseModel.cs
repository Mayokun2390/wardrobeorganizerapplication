using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Dtos
{
    public class CartResponseModel
    {
        public Guid Id{ get; set; } 
        public string NameOfProduct{ get; set; } = default!;
        public int Quantity{ get; set; }
        public decimal TotalPrice{ get; set; }
    }


    public class CartRequestModel
    {
        public Guid Id{ get; set; } 
        public int Quantity{ get; set; }
        public Guid ProductId{ get; set; }
        public string NameOfProduct{ get; set; } = default!;
        public decimal TotalPrice{ get; set; }

    }
}