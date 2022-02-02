using AntimalnikAPI.Enums;
using AntimalnikAPI.Models;
using System.Collections.Generic;

namespace AntimalnikAPI.ViewModels
{
    public class UserInputViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}