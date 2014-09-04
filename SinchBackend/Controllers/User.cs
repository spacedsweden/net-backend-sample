using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinchBackend.Controllers {
    public class AuthenticatedUser : User {
        public string Token { get; set; }
    }

    public class User {
        public string Username { get; set; }
        //public string Email { get; set; }
        public string Name { get; set; }
        //public string LastSeen { get; set; }

    }
}
