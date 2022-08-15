using AntimalnikAPI.BLL.Interfaces;
using AntimalnikAPI.Common;
using AntimalnikAPI.Controllers;
using AntimalnikAPI.DAL.Models;
using AntimalnikAPI.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AntimalnikAPI.Tests
{
    public class UserControllerShould
    {
        private Mock<IUserService> _MockedUserService;
        private Mock<IMapper> _MockedMapper;
        private UserController _UserController;

        private UserInputViewModel sampleUserView =>
            new UserInputViewModel
            {
                UserName = "test",
                FirstName = "test",
                LastName = "test",
                Email = "test@test.com",
                Password = "test"
            };

        private ApplicationUser sampleUser =>
            new ApplicationUser
            {
                UserName = "test",
                FirstName = "test",
                LastName = "test",
                Email = "test@test.com",
                Role = RoleType.User,
                Posts = new List<Post>()
            };

        private LoginModel sampleLoginData =>
            new LoginModel
            {
                UserName = "test",
                Password = "test"
            };



        [SetUp]
        public void Setup()
        {
            _MockedUserService = new Mock<IUserService>();
            _MockedMapper = new Mock<IMapper>();
            _UserController = new UserController(_MockedUserService.Object, _MockedMapper.Object);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_UserController, Is.TypeOf<UserController>());
            Assert.IsNotNull(_UserController);
        }

        [Test]
        public void ThrowNullReferenceExceptionWhenUserServiceIsNull()
        {
            _MockedUserService = null;
            Assert.Throws<NullReferenceException>(() => new UserController(_MockedUserService.Object, _MockedMapper.Object));
        }

        [Test]
        public void ThrowNullReferenceExceptionWhenMapperServiceIsNull()
        {
            _MockedMapper = null;
            Assert.Throws<NullReferenceException>(() => new UserController(_MockedUserService.Object, _MockedMapper.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenUserServiceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UserController(null, _MockedMapper.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenMapperIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UserController(_MockedUserService.Object, null));
        }

        [Test]
        public void GetUsersTest()
        {
            var result = _UserController.GetUsers();

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void GetUserTest()
        {
            string userName = "test";
            var result = _UserController.GetUser(userName);

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void GetUserPostsTest()
        {
            string userName = "test";
            var result = _UserController.GetUserPosts(userName);

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void AddUserTest()
        {
            var result = _UserController.AddUser(sampleUserView);

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void EditUserTest()
        {
            var result = _UserController.EditUser(sampleUser);

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void DeletePostTest()
        {
            string userName = "test";
            var result = _UserController.DeleteUser(userName);

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void LoginTest()
        {
            var result = _UserController.Login(sampleLoginData);

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
    }
}