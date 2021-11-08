using AntimalnikAPI.Data;
using AntimalnikAPI.Models;
using AntimalnikAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public Task<List<ApplicationUser>> GetUsers() => this._context.Users.ToListAsync();

        public Task<ApplicationUser> GetUser(string userName) => this._context.Users.SingleOrDefaultAsync(x => x.UserName == userName);

        public List<Post> GetUserPosts(string userName) => GetUser(userName).Result.Posts.ToList();

        public async Task AddUser(ApplicationUser user)
        {
            this._context.Users.Add(user);
            await this._context.SaveChangesAsync();
        }

        public async Task EditUser(ApplicationUser user)
        {
            this._context.Users.Update(user);
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteUser(string userName)
        {
            var user = GetUser(userName).Result;
            this._context.Users.Remove(user);
            await this._context.SaveChangesAsync();
        }
    }
}
