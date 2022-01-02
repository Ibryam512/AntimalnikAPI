using AntimalnikAPI.Data;
using AntimalnikAPI.Models;
using AntimalnikAPI.ViewModels;
using AntimalnikAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        public Task<List<ApplicationUser>> GetUsers() => this._context.Users.ToListAsync();

        public Task<ApplicationUser> GetUser(string userName) => this._context.Users.SingleOrDefaultAsync(x => x.UserName == userName);

        public List<Post> GetUserPosts(string userName) => GetUser(userName).Result.Posts.ToList();

        public bool AddUser(ApplicationUser user, string password)
        {
            var result = _userManager.CreateAsync(user, password).Result;
            if (result.Succeeded) return true;
            return false;
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

        public async Task<SignInResult> Login(LoginModel input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, lockoutOnFailure: false);
            return result;
        }
    }
}
