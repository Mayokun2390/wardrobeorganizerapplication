using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WardrobeOrganizerApp.Dtos
{
    public class Response<T>
    {
        public string Message {get; set;} = default!;
        public bool Status {get; set;} 
        public T Value {get; set;} = default!;
    }
}