using AntimalnikAPI.Services.Interfaces;
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
        private readonly IPostService _service;

        public PostController(IPostService service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            var posts = this._service.GetPosts().Result;
            return new JsonResult(posts);
        }

        [HttpGet("{id}")]
        public IActionResult Post(string id)
        {
            var post = this._service.GetPost(id).Result;
            return new JsonResult(post);
        }

        [HttpPost]
        public IActionResult AddPost(Post post)
        {
            this._service.AddPost(post);
            return new JsonResult($"Post with title {post.Title} added successfully.");
        }

        [HttpPut]
        public IActionResult EditPost(Post post)
        {
            this._service.EditPost(post);
            return new JsonResult($"Post with title {post.Title} updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(string id)
        {
            this._service.DeletePost(id);
            return new JsonResult("The post is deleted successfully.");
        }
    }
}
