using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AntimalnikAPI.Data;
using AntimalnikAPI.Models;

namespace AntimalnikAPI.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            var posts = this._context.Posts.ToList();
            return new JsonResult(posts);
        }

        [HttpGet("{title}")]
        public IActionResult Post(string title)
        {
            var post = this._context.Posts.SingleOrDefault(x => x.Title == title);
            return new JsonResult(post);
        }

        [HttpPost]
        public IActionResult AddPost(Post post)
        {
            this._context.Posts.Add(post);
            this._context.SaveChanges();
            return new JsonResult($"Post with title {post.Title} added successfully.");
        }

        [HttpPut]
        public IActionResult EditPost(Post post)
        {
            this._context.Posts.Update(post);
            this._context.SaveChanges();
            return new JsonResult($"Post with title {post.Title} updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(string id)
        {
            var post = this._context.Posts.SingleOrDefault(x => x.Id == id);
            this._context.Posts.Remove(post);
            this._context.SaveChanges();
            return new JsonResult("The post is deleted successfully.");
        }
    }
}
