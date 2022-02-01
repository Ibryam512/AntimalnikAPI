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
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        public PostService(ApplicationDbContext context, IUserService userService)
        {
            this._context = context;
            this._userService = userService;
        }
        public Task<List<Post>> GetPosts() => this._context.Posts.Include(post => post.User).ToListAsync();

        public Task<Post> GetPost(string id) => this._context.Posts.SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddPost(Post post)
        {
            this._context.Posts.Add(post);
            Console.WriteLine("adding in posts");
            this._userService.GetUser(post.User.UserName).Result.Posts.Add(post);
            Console.WriteLine("adding in user");
            await this._context.SaveChangesAsync();
        }

        public async Task EditPost(Post post)
        {
            this._context.Posts.Update(post);
            await this._context.SaveChangesAsync();
        }

        public async Task DeletePost(string id)
        {
            var post = GetPost(id).Result;
            this._context.Posts.Remove(post);
            await this._context.SaveChangesAsync();
        }
    }
}
