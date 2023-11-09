using System;
using System.Collections.Generic;
using System.Text;

namespace ReciPie.Models
{
    public class UserCredentials
    {
        public string idToken { get; set; }
        public string refreshToken { get; set; }
        public int expiresIn { get; set; }
        public string Created { get; set; }
        public User User { get; set; }
    }
}