using AntimalnikAPI.Data;
using AntimalnikAPI.Models;
using AntimalnikAPI.Services.Interfaces;
using AntimalnikAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public PostController(IPostService service, IUserService userService, IMapper mapper)
        {
            this._service = service;
            this._userService = userService;
            this._mapper = mapper;
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
        public IActionResult AddPost(PostInputViewModel postView)
        {
            var post = this._mapper.Map<Post>(postView);
            post.User = this._userService.GetUser(postView.UserName).Result;
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
