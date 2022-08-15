using AntimalnikAPI.BLL;
using AntimalnikAPI.Common;
using AntimalnikAPI.DAL;
using AntimalnikAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntimalnikAPI.Tests
{
    public class MessageServiceShould
    {
        private AntimalnikDbContext _Context;
        private MessageService _MessageService;

        private ApplicationUser userOne =>
            new ApplicationUser
            {
                UserName = "test1",
                FirstName = "test",
                LastName = "test",
                Email = "test@test.com",
                Role = RoleType.User,
                Posts = new List<Post>()
            };

        private ApplicationUser userTwo =>
            new ApplicationUser
            {
                UserName = "test2",
                FirstName = "test",
                LastName = "test",
                Email = "test@test.com",
                Role = RoleType.User,
                Posts = new List<Post>()
            };

        private Message sampleMessage =>
            new Message
            {
                Sender = userOne,
                Reciever = userTwo,
                Text = "test",
                CreationDate = new DateTime()
            };


        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AntimalnikDbContext>();
            optionsBuilder.UseInMemoryDatabase("Antimalnik");
            _Context = new AntimalnikDbContext(optionsBuilder.Options);
            _MessageService = new MessageService(_Context);
            _Context.Users.Add(userOne);
            _Context.Users.Add(userTwo);
            _Context.Messages.Add(sampleMessage);
            _Context.SaveChanges();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_MessageService, Is.TypeOf<MessageService>());
            Assert.IsNotNull(_MessageService);
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MessageService(null));
        }

        [Test]
        public void GetSentMessagesTest()
        {
            string userName = _Context.Users.First().UserName;
            var result = _MessageService.GetSentMessages(userName).Result;

            Assert.IsInstanceOf<List<Message>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<Message>>());
        }

        [Test]
        public void GetRecievedMessagesTest()
        {
            string userName = _Context.Users.First().UserName;
            var result = _MessageService.GetRecievedMessages(userName).Result;

            Assert.IsInstanceOf<List<Message>>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<List<Message>>());
        }

        [Test]
        public void SendMessageTest()
        {
            _MessageService.SendMessage(sampleMessage);
            var message = _Context.Messages.Last();

            Assert.IsNotNull(message);
            Assert.IsInstanceOf<Message>(message);
            Assert.That(message, Is.TypeOf<Message>());
            Assert.That(message.Text, Is.EqualTo("test"));
        }

        [Test]
        public void DeleteMessageTest()
        {
            int count = _Context.Messages.ToList().Count;
            var message = _Context.Messages.First();
            _MessageService.DeleteMessage(message.Id);
            var messages = _Context.Messages.ToList();

            Assert.IsNotNull(messages);
            Assert.IsInstanceOf<List<Message>>(messages);
            Assert.That(messages, Is.TypeOf<List<Message>>());
            Assert.That(messages.Count, Is.Not.EqualTo(count));
        }
    }
}