using AntimalnikAPI.Enums;
using AntimalnikAPI.Models;
using AntimalnikAPI.Services.Interfaces;
using AntimalnikAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(IUserService service, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this._service = service;
            this._userManager = userManager;
            this._mapper = mapper;
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
        public IActionResult AddUser(UserInputViewModel userView)
        {
            var user = _mapper.Map<ApplicationUser>(userView);
            var result = this._service.AddUser(user, userView.Password);
            if (result)
                return new JsonResult($"The user with username {user.UserName} was added successfully.");
            return new JsonResult("There was an error.");
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

        [HttpPost("/login")]
        public IActionResult Login(LoginModel input)
        {
            var logged = this._service.Login(input).Result;
            if (!logged.Succeeded)
            {
                return Ok(new { status = 401, isSuccess = false, message = "Грешно потребителско име или парола", });
            }
            var user = _userManager.GetUserAsync(User).Result;
            var userView = _mapper.Map<UserViewModel>(user);
            return Ok(new { status = 200, isSuccess = true, message = "Потребителят влезе успешно", UserDetails = userView });
        }
    }
}
