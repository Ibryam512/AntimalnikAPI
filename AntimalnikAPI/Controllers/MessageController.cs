using AntimalnikAPI.BLL.Interfaces;
using AntimalnikAPI.DAL.Models;
using AntimalnikAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AntimalnikAPI.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;
        private readonly IUserService _userService;

        public MessageController(IMessageService service, IUserService userService)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
            this._userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("{userName}/sent")]
        public IActionResult GetSentMessages(string userName)
        {
            var messages = this._service.GetSentMessages(userName).Result;
            return new JsonResult(messages);
        }

        [HttpGet("{userName}/recieved")]
        public IActionResult GetRecievedMessages(string userName)
        {
            var messages = this._service.GetRecievedMessages(userName).Result;
            return new JsonResult(messages);
        }

        [HttpPost("send")]
        public IActionResult SendMessage(MessageInputViewModel messageView)
        {
            var sender = this._userService.GetUser(messageView.Sender).Result;
            var reciever = this._userService.GetUser(messageView.Reciever).Result;
            var message = new Message
            {
                Sender = sender,
                Reciever = reciever,
                Text = messageView.Text,
                CreationDate = DateTime.Now
            };
            this._service.SendMessage(message);
            return new JsonResult("The message is sent successfully.");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteMessage(string id)
        {
            this._service.DeleteMessage(id);
            return new JsonResult("The message has been deleted successfully.");
        }

        [HttpPost("send/question")]
        public IActionResult SendQuestion(MessageInputViewModel messageView)
        {
            var sender = this._userService.GetUser(messageView.Sender).Result;
            var message = new Message
            {
                Sender = sender,
                Text = messageView.Text,
                CreationDate = DateTime.Now
            };
            this._service.SendQuestion(message);
            return new JsonResult("The question is sent successfully.");
        }

    }
}
