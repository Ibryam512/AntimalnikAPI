using AntimalnikAPI.DAL.Models;
using AntimalnikAPI.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AntimalnikAPI.DAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AntimalnikDbContext _context;

        public PostRepository(AntimalnikDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Post> GetPosts() => this._context.Posts.ToList();

        public List<Post> GetPosts(Expression<Func<Post, bool>> predicate) => this._context.Posts.Where(predicate).ToList();

        public Post GetPost(string id) => this._context.Posts.SingleOrDefault(x => x.Id == id);

        public Post GetPost(Expression<Func<Post, bool>> predicate) => this._context.Posts.SingleOrDefault(predicate);

        public void AddPost(Post post)
        {
            this._context.Posts.Add(post);
            this._context.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            this._context.Posts.Update(post);
            this._context.SaveChanges();
        }

        public void RemovePost(Post post)
        {
            this._context.Posts.Remove(post);
            this._context.SaveChanges();
        }
    }
}
