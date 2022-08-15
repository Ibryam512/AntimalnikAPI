using AntimalnikAPI.BLL.Interfaces;
using AntimalnikAPI.DAL.Models;
using AntimalnikAPI.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AntimalnikAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service)); ;
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
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
            try
            {
                if (this._service.DoesUserNameAlreadyExist(userView.UserName))
                {
                    return new JsonResult(new { message = "Потребител със същото потребителско име вече съществува!", status = 401 });
                }

                if (this._service.DoesEmailAlreadyExist(userView.Email))
                {
                    return new JsonResult(new { message = "Потребител със същия имейл вече съществува!", status = 401 });
                }

                var user = _mapper.Map<ApplicationUser>(userView);
                user.EmailConfirmed = true;
                var result = this._service.AddUser(user, userView.Password);
                if (result)
                    return new JsonResult(new { message = $"The user with username {user.UserName} was added successfully.", status = 200 });
                return new JsonResult(new { message = "There was an error.", status = 401 });
            }
            catch (NullReferenceException)
            {
                return new JsonResult(new { message = "There was an error, please try again later.", status = 401 });
            }
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

        [HttpPost("login")]
        public IActionResult Login(LoginModel input)
        {
            try
            {
                var logged = this._service.Login(input).Result;
                if (!logged.Succeeded)
                {
                    return Ok(new { status = 401, isSuccess = false, message = "Грешно потребителско име или парола", });
                }
                var user = _service.GetUser(input.UserName).Result;
                var userView = _mapper.Map<UserViewModel>(user);
                return Ok(new { status = 200, isSuccess = true, message = "Потребителят влезе успешно", UserDetails = userView });
            }
            catch (NullReferenceException)
            {
                return Ok(new { status = 404, message = "Потребител с тези данни не съществува." });
            }
        }
    }
}
