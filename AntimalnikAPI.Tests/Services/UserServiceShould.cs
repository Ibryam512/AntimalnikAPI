using AntimalnikAPI.DAL;
using AntimalnikAPI.DAL.Enums;
using AntimalnikAPI.DAL.Models;
using AntimalnikAPI.Services;
using AntimalnikAPI.Tests.FakeClasses;
using AntimalnikAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.Tests
{
    public class UserServiceShould
    {

        private FakeSignInManager _SignInManager;
        private FakeUserManager _UserManager;
        private AntimalnikDbContext _Context;
        private UserService _UserService;

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
            var optionsBuilder = new DbContextOptionsBuilder<AntimalnikDbContext>();
            optionsBuilder.UseInMemoryDatabase("Antimalnik");
            _Context = new AntimalnikDbContext(optionsBuilder.Options);
            _SignInManager = new FakeSignInManager(_Context);
            _UserManager = new FakeUserManager(_Context);
            _UserService = new UserService(_Context, _SignInManager, _UserManager);
            _Context.Users.Add(sampleUser);
            _Context.SaveChanges();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_UserService, Is.TypeOf<UserService>());
            Assert.IsNotNull(_UserService);
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(null, _SignInManager, _UserManager));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenSignInManagerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(_Context, null, _UserManager));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenUserManagerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UserService(_Context, _SignInManager, null));
        }

        [Test]
        public void GetUsersTest()
        {
            var result = _UserService.GetUsers().Result;

            Assert.IsInstanceOf<List<ApplicationUser>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<ApplicationUser>>());
        }

        [Test]
        public void GetUserTest()
        {
            string userName = _Context.Users.First().UserName;
            var result = _UserService.GetUser(userName).Result;

            Assert.IsInstanceOf<ApplicationUser>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ApplicationUser>());
        }

        [Test]
        public void GetUserPostsTest()
        {
            string userName = _Context.Users.First().UserName;
            var result = _UserService.GetUserPosts(userName);

            Assert.IsInstanceOf<List<Post>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<Post>>());
        }

        [Test]
        public void AddUserTest()
        {
            string password = "test";
            _UserService.AddUser(sampleUser, password);
            var user = _Context.Users.Last();

            Assert.IsNotNull(user);
            Assert.IsInstanceOf<ApplicationUser>(user);
            Assert.That(user, Is.TypeOf<ApplicationUser>());
            Assert.That(user.UserName, Is.EqualTo("test"));
        }

        [Test]
        public void EditPostTest()
        {
            var count = _Context.Users.ToList().Count;
            var user = _Context.Users.First();
            user.Role = RoleType.Moderator;
            _UserService.EditUser(user);
            var editedUser = _Context.Users.First();
            var users = _Context.Users.ToList();

            Assert.IsNotNull(editedUser);
            Assert.IsInstanceOf<ApplicationUser>(editedUser);
            Assert.That(editedUser, Is.TypeOf<ApplicationUser>());
            Assert.That(editedUser.Role, Is.EqualTo(RoleType.Moderator));
            Assert.That(users.Count, Is.EqualTo(count));
        }

        [Test]
        public void DeleteUserTest()
        {
            int count = _Context.Users.ToList().Count;
            var user = _Context.Users.First();
            _UserService.DeleteUser(user.UserName);
            var users = _Context.Users.ToList();

            Assert.IsNotNull(users);
            Assert.IsInstanceOf<List<ApplicationUser>>(users);
            Assert.That(users, Is.TypeOf<List<ApplicationUser>>());
            Assert.That(users.Count, Is.Not.EqualTo(count));
        }

        [Test]
        public void LoginTest()
        {
            var result = _UserService.Login(sampleLoginData);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<SignInResult>>(result);
            Assert.That(result, Is.TypeOf<Task<SignInResult>>());
        }
    }
}