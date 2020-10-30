using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class UsersViewModel
    {
        public int Id { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string UserName { set; get; }
        public string ActiveUser { set; get; }
        public string isAdmin { set; get; }
        public string MobileNumber { set; get; }
    }
}