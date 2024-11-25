using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;
using WardrobeOrganizerApp.Enums;

namespace WardrobeOrganizerApp.Dtos
{
    public class AddToCartResponseModel
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }

    public class CreateOrderRequest
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

    }
}