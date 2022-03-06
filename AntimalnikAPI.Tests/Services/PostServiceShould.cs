using AntimalnikAPI.Data;
using AntimalnikAPI.Enums;
using AntimalnikAPI.Models;
using AntimalnikAPI.Services;
using AntimalnikAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntimalnikAPI.Tests
{
    public class PostServiceShould
    {
        private Mock<IUserService> _MockedUserService;
        private ApplicationDbContext _Context;
        private PostService _PostService;

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
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("Antimalnik");
            _Context = new ApplicationDbContext(optionsBuilder.Options);
            _MockedUserService = new Mock<IUserService>();
            _PostService = new PostService(_Context, _MockedUserService.Object);
            _Context.Posts.Add(samplePost);
            _Context.SaveChanges();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_PostService, Is.TypeOf<PostService>());
            Assert.IsNotNull(_PostService);
        }

        [Test]
        public void ThrowNullReferenceExceptionWhenUserServiceIsNull()
        {
            _MockedUserService = null;
            Assert.Throws<NullReferenceException>(() => new PostService(_Context, _MockedUserService.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PostService(null, _MockedUserService.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenUserServiceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PostService(_Context, null));
        }

        [Test]
        public void GetPostsTest()
        {
            var result = _PostService.GetPosts().Result;

            Assert.IsInstanceOf<List<Post>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<Post>>());
        }

        [Test]
        public void GetPostTest()
        {
            string postId = _Context.Posts.First().Id;
            var result = _PostService.GetPost(postId).Result;

            Assert.IsInstanceOf<Post>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<Post>());
        }

        [Test]
        public void AddPostTest()
        {
            _PostService.AddPost(samplePost);
            var post = _Context.Posts.Last();

            Assert.IsNotNull(post);
            Assert.IsInstanceOf<Post>(post);
            Assert.That(post, Is.TypeOf<Post>());
            Assert.That(post.Title, Is.EqualTo("test"));
        }

        [Test]
        public void EditPostTest()
        {
            var count = _Context.Posts.ToList().Count;
            var post = _Context.Posts.First();
            post.Title = "editedTitle";
            _PostService.EditPost(post);
            var editedPost = _Context.Posts.First();
            var posts = _Context.Posts.ToList();

            Assert.IsNotNull(editedPost);
            Assert.IsInstanceOf<Post>(editedPost);
            Assert.That(editedPost, Is.TypeOf<Post>());
            Assert.That(editedPost.Title, Is.EqualTo("editedTitle"));
            Assert.That(posts.Count, Is.EqualTo(count));
        }

        [Test]
        public void DeletePostTest()
        {
            int count = _Context.Posts.ToList().Count;
            var post = _Context.Posts.First();
            _PostService.DeletePost(post.Id);
            var posts = _Context.Posts.ToList();

            Assert.IsNotNull(posts);
            Assert.IsInstanceOf<List<Post>>(posts);
            Assert.That(posts, Is.TypeOf<List<Post>>());
            Assert.That(posts.Count, Is.Not.EqualTo(count));
        }
    }
}