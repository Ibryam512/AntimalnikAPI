using AntimalnikAPI.BLL.Interfaces;
using AntimalnikAPI.DAL;
using AntimalnikAPI.DAL.Models;
using AntimalnikAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.BLL
{
    public class UserService : IUserService
    {
        private readonly AntimalnikDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(AntimalnikDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public Task<List<ApplicationUser>> GetUsers() => this._context.Users.ToListAsync();

        public Task<ApplicationUser> GetUser(string userName)
        {
            var user = this._context.Users.Include(user => user.Posts).FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NullReferenceException("User does not exist.");
            }
            return user;
        }

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
            this._context.SaveChanges();
        }

        public async Task DeleteUser(string userName)
        {
            var user = GetUser(userName).Result;
            this._context.Users.Remove(user);
            this._context.SaveChanges();
        }

        public async Task<SignInResult> Login(LoginModel input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.UserName, input.Password, false, lockoutOnFailure: false);
            if (result == null)
            {
                throw new NullReferenceException();
            }
            return result;
        }

        public bool DoesUserNameAlreadyExist(string userName)
        {
            var userNames = this._context.Users.Select(x => x.UserName).ToList();
            return userNames.Contains(userName);
        }

        public bool DoesEmailAlreadyExist(string email)
        {
            var emails = this._context.Users.Select(x => x.Email).ToList();
            return emails.Contains(email);
        }
    }
}
