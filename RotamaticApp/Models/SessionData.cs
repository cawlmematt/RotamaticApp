using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RotamaticApp.Models
{
    public class SessionData
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public List<string> UserRoles { get; set; }
    }
}