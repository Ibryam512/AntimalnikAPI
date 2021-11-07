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
        public PostService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public Task<List<Post>> GetPosts() => this._context.Posts.ToListAsync();

        public Task<Post> GetPost(string id) => this._context.Posts.SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddPost(Post post)
        {
            this._context.Posts.Add(post);
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
