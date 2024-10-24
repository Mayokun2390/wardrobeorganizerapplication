using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WardrobeOrganizerApp.Entities
{
    public class CartProduct
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CartId { get; set; } =  Guid.NewGuid().ToString();
        public Cart Cart { get; set; } = default!;
        public string ProductId { get; set; } =  Guid.NewGuid().ToString();
        public Product Product { get; set; } = default!;
    }
}