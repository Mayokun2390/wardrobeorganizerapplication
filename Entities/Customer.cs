using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Enums;

namespace WardrobeOrganizerApp.Entities
{
    public class Customer
    {
        public Guid Id{ get; set; } = Guid.NewGuid();
        public string FirstName{ get; set; } = default!;
        public string LastName{ get; set;} = default!;
        public string PhoneNumber{get; set;} = default!;
        public string Email{ get; set; } = default!;
        public string Password{ get; set; } = default!;
        public CustomerStatus CustomerStatus{get; set; }

        public string RoleName{ get; set; } = default!;
        public ICollection<Order> Order { get; set; } = new HashSet<Order>();
        public ICollection<Outfits> Outfits { get; set; } = new HashSet<Outfits>();
        public ICollection<ClothingItems> ClothingItems { get; set; } = new HashSet<ClothingItems>();
        public ICollection<Payment> Payment { get; set; } = new HashSet<Payment>();
        // public ChartBot chartBot {get; set;}
        public string ChartBotId {get; set;} = default!;
    }
}