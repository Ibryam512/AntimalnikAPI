using AntimalnikAPI.Models;
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
        Task AddUser(ApplicationUser user);
        Task EditUser(ApplicationUser user);
        Task DeleteUser(string userName);
    }
}
