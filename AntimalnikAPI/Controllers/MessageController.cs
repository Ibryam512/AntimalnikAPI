using AntimalnikAPI.Data;
using AntimalnikAPI.Models;
using AntimalnikAPI.Services.Interfaces;
using AntimalnikAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            this._service = service;
            this._userService = userService;
        }

        [HttpGet("sent")]
        public IActionResult GetSentMessages(string userName)
        {
            var messages = this._service.GetSentMessages(userName).Result;
            return new JsonResult(messages);
        }

        [HttpGet("recieved")]
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
                SentDate = DateTime.Now
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
        
    }
}
