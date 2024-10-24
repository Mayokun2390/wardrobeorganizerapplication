using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Dtos
{
    public class CustomerResponseModel
    {
        public Guid Id{ get; set; } 
        public string FirstName{ get; set; } = default!;
        public string LastName{ get; set;} = default!;
        public string PhoneNumber{get; set;} = default!;
        public string Email{ get; set; } = default!;
        public string RoleName{ get; set; } = default!;
        public ICollection<Order> Order { get; set; } = new HashSet<Order>();
        public ICollection<Outfits> Outfits { get; set; } = new HashSet<Outfits>();
        public ICollection<ClothingItems> ClothingItems { get; set; } = new HashSet<ClothingItems>();
        public ICollection<Payment> Payment { get; set; } = new HashSet<Payment>();
        public string ChartBotId {get; set;}  = default!;
    }


    public class CustomerRequestModel
    {
        public string FirstName{ get; set; } = default!;
        public string LastName{ get; set;} = default!;
        public string PhoneNumber{get; set;} = default!;
        public string Email{ get; set; } = default!;
        public string Password{ get; set; } = default!;
    }
}