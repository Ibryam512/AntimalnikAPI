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
    public class MessageControllerShould
    {
        private Mock<IMessageService> _MockedMessageService;
        private Mock<IUserService> _MockedUserService;
        private MessageController _MessageController;

       private MessageInputViewModel sampleMessageView =>
			new MessageInputViewModel {
				Sender = "test1",
                Reciever = "test2",
                Text = "test"
			};

        [SetUp]
        public void Setup()
        {
            _MockedMessageService = new Mock<IMessageService>();
            _MockedUserService = new Mock<IUserService>();
            _MessageController = new MessageController(_MockedMessageService.Object, _MockedUserService.Object);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.That(_MessageController, Is.TypeOf<MessageController>());
            Assert.IsNotNull(_MessageController);
        }

        [Test]
        public void ThrowNullReferenceExceptionWhenMessageServiceIsNull()
        {
            _MockedMessageService = null;
            Assert.Throws<NullReferenceException>(() => new MessageController(_MockedMessageService.Object, _MockedUserService.Object));
        }

        [Test]
        public void ThrowNullReferenceExceptionWhenUserServiceIsNull()
        {
            _MockedUserService = null;
            Assert.Throws<NullReferenceException>(() => new MessageController( _MockedMessageService.Object, _MockedUserService.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenMessageServiceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MessageController(null, _MockedUserService.Object));
        }

        [Test]
        public void ThrowArgumentNullExceptionWhenUserServiceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MessageController(_MockedMessageService.Object, null));
        }

        [Test]
        public void GetSentMessagesTest()
        {
            string userName = "test";
            var result = _MessageController.GetSentMessages(userName);

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void GetRecievedMessagesTest()
        {
            string userName = "test";
            var result = _MessageController.GetRecievedMessages(userName);

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void SendMessageTest()
        {
            var result = _MessageController.SendMessage(sampleMessageView);

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void DeleteMessageTest()
        {
            string messageId = "test";
            var result = _MessageController.DeleteMessage(messageId);

            Assert.IsInstanceOf<JsonResult>(result);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<JsonResult>());
        }
    }
}