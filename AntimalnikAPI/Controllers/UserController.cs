using AntimalnikAPI.Enums;
using AntimalnikAPI.Models;
using AntimalnikAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            this._service = service;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = this._service.GetUsers().Result;
            return new JsonResult(users);
        }

        [HttpGet("{userName}")]
        public IActionResult GetUser(string userName)
        {
            var user = this._service.GetUser(userName).Result;
            return new JsonResult(user);
        }

        [HttpGet("{userName}/posts")]
        public IActionResult GetUserPosts(string userName)
        {
            var posts = this._service.GetUserPosts(userName);
            return new JsonResult(posts);
        }

        [HttpPost]
        public IActionResult AddUser(ApplicationUser user)
        {
            this._service.AddUser(user);
            return new JsonResult($"The user with username {user.UserName} was added successfully.");
        }

        [HttpPut]
        public IActionResult EditUser(ApplicationUser user)
        {
            this._service.EditUser(user);
            return new JsonResult($"The user with username {user.UserName} was edited successfully.");
        }

        [HttpDelete("{userName}")]
        public IActionResult DeleteUser(string userName)
        {
            this._service.DeleteUser(userName);
            return new JsonResult("The user was deleted successfully.");
        }
    }
}
