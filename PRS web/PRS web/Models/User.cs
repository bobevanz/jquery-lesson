using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRS_web.Models
{
    public class User{
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsReviewer { get; set; }
        public bool IsAdmin { get; set; }
    }
    }
    
