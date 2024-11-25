using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Enums;

namespace WardrobeOrganizerApp.Entities
{
    public class Cart
    {
        public Guid Id { get; set; } =  Guid.NewGuid();
        public Guid UserId { get; set; } =  Guid.NewGuid();
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        
    }
}