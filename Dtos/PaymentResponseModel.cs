using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Dtos
{
    public class PaymentResponseModel
    {
        public Guid Id{ get; set; } 
        public decimal Amount{ get; set; }
        public enum Status;
        public DateTime DateCreated{get; set; }
        public Customer Customer {get; set;}
        public string CustomerId {get; set;}
        public Order Order {get; set;}
        public string OrderId {get; set;}
    }


    public class PaymentRequestModel
    {
        public decimal Amount{ get; set; }
    }
}