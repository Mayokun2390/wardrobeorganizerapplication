using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WardrobeOrganizerApp.Entities;

namespace WardrobeOrganizerApp.Dtos
{
    public class ChartBotResponseModel
    {
        public Guid Id{ get; set; } 
        public string MessageText {get; set;} = default!;
        public string ResponseText {get; set;} = default!;
        public DateTime DateCreated{get; set; }
        public Customer Customer {get; set;}
    }


    public class ChartBotRequestModel
    {
        public Guid Id{ get; set; } 
        public string MessageText {get; set;} = default!;
        public string ResponseText {get; set;} = default!;
    }
}