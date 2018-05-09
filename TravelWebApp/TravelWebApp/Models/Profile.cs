using System;
using System.Collections.Generic;
using System.Text;

namespace TravelWebApp.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
