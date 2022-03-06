using AntimalnikAPI.Controllers;
using AntimalnikAPI.Enums;
using AntimalnikAPI.Models;
using AntimalnikAPI.Services.Interfaces;
using AntimalnikAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntimalnikAPI.Tests
{
    public class PostControllerShould
    {
        private Mock<IPostService> _MockedPostService;
        private Mock<IUserService> _MockedUserService;
        private Mock<IMapper> _MockedMapper;
        private PostController _PostController;

        private PostInputViewModel samplePostView =>
			new PostInputViewModel {
				PostType = PostType.Ad,
                Title = "test",
                Description = "test",
                Price = 0.01,
                UserName = "tester"
			};

        private Post samplePost =>
			new Post {
				PostType = PostType.Ad,
                Title = "test",
                Description = "test",
                Price = 0.01,
                DeleteDate = new DateTime(),
                Image = "image.jpeg",
                AddDate = new DateTime()
			};  


        [SetUp]
        public void Setup()
        {
            _MockedPostService = new Mock<IPostService>();
            _MockedUserService = new Mock<IUserService>();
            _MockedMapper = new Mock<IMapper>();
            _PostController = new PostController(_MockedPostService.Object, _MockedUserService.Object, _MockedMapper.Object);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_PostController, Is.TypeOf<PostController>());
            Assert.IsNotNull(_PostController);
        }

        [Test]
        public void ThrowNullReferenceExceptionWhenPostServiceIsNull()
        {
            _MockedPostService = null;
            Assert.Throws<NullReferenceException>(() => new PostController(_MockedPostService.Object, _MockedUserService.Object, _MockedMapper.Object));
        }

        [Test]
        public void ThrowNullReferenceExceptionWhenUserServiceIsNull()
        {
            _MockedUserService = null;
            Assert.Throws<NullReferenceException>(() => new PostController(_MockedPostService.Object, _MockedUserService.Object, _MockedMapper.Object));
        }

        [Test]
        public void ThrowNullReferenceExceptionWhenMapperIsNull()
        {
            _MockedMapper = null;
            Assert.Throws<NullReferenceException>(() => new PostController(_MockedPostService.Object, _MockedUserService.Object, _MockedMapper.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenPostServiceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PostController(null, _MockedUserService.Object, _MockedMapper.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenUserServiceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PostController(_MockedPostService.Object, null, _MockedMapper.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenMapperIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PostController(_MockedPostService.Object, _MockedUserService.Object, null));
        }

        [Test]
        public void GetPostsTest()
        {
            var result = _PostController.GetPosts();

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void GetPostTest()
        {
            string postId = "test";
            var result = _PostController.Post(postId);

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void AddPostTest()
        {
            var result = _PostController.AddPost(samplePostView);

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void EditPostTest()
        {
            var result = _PostController.EditPost(samplePost);

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void DeletePostTest()
        {
            string postId = "test";
            var result = _PostController.DeletePost(postId);

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }
    }
}