using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static Xamarin.Essentials.Permissions;

namespace ReciPie.Models
{
    public class AddUserInformation
    {
        // public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public string Birthdate { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
    }
}
