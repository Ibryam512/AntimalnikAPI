using AntimalnikAPI.Enums;
using AntimalnikAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RoleType Role { get; set; }
        public List<Post> Posts { get; set; }
    }
}
