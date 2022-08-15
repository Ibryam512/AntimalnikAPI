using AntimalnikAPI.DAL.Models;
using AntimalnikAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntimalnikAPI.BLL.Interfaces
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
        bool DoesUserNameAlreadyExist(string userName);
        bool DoesEmailAlreadyExist(string email);
    }
}
