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

        [HttpPost]
        public IActionResult AddPost(Post post)
        {
            this._context.Posts.Add(post);
            this._context.SaveChanges();
            return new JsonResult($"Post with title {post.Title} added successfully");
        }
    }
}
