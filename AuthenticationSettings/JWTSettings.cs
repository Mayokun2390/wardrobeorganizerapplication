using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WardrobeOrganizerApp.AuthenticationSettings
{
    public class JWTSettings
    {
        public string SecurityKey { get; set; } = default!;
        public string Isseur { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public double ExpiratinTime {get; set;}
    }
}