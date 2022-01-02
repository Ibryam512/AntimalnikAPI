using AntimalnikAPI.Models;
using AntimalnikAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetUser(string userName);
        List<Post> GetUserPosts(string userName);
        bool AddUser(ApplicationUser user, string password);
        Task EditUser(ApplicationUser user);
        Task DeleteUser(string userName);
        Task<SignInResult> Login(LoginModel input);
    }
}
