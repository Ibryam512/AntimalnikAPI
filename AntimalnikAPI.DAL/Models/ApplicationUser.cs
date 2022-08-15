using AntimalnikAPI.DAL.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AntimalnikAPI.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public RoleType Role { get; set; }
        public List<Post> Posts { get; set; }
        public List<Message> SentMessages { get; set; }
        public List<Message> RecievedMessages { get; set; }
    }
}
