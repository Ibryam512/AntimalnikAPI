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
        private readonly IMapper _mapper;

        public MessageController(IMessageService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [HttpGet("sent")]
        public IActionResult GetSentMessages()
        {
            string userName = "";
            var messages = this._service.GetSentMessages(userName);
            return new JsonResult(messages);
        }

        [HttpGet("recieved")]
        public IActionResult GetRecievedMessages()
        {
            string userName = "";
            var messages = this._service.GetRecievedMessages(userName);
            return new JsonResult(messages);
        }

        
    }
}
