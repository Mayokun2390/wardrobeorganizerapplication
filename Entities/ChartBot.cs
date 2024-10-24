using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WardrobeOrganizerApp.Entities
{
    public class ChartBot
    {
        public Guid Id{ get; set; } 
        public string MessageText {get; set;} = default!;
        public string ResponseText {get; set;} = default!;
        public DateTime DateCreated{get; set; }
        public Customer Customer {get; set;} = default!;
        // public string CustomerId {get; set;} 

    }
}