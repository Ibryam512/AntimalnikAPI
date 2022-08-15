using AntimalnikAPI.BLL.Interfaces;
using AntimalnikAPI.DAL.Models;
using AntimalnikAPI.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;

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
            this._service = service ?? throw new ArgumentNullException(nameof(service));
            this._userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            try
            {
                var post = this._mapper.Map<Post>(postView);
                post.Creator = this._userService.GetUser(postView.UserName).Result;
                this._service.AddPost(post);
                return new JsonResult($"Post with title {post.Title} added successfully.");
            }
            catch (NullReferenceException)
            {
                return new JsonResult($"The user with username {postView.UserName} does not exist!");
            }
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
