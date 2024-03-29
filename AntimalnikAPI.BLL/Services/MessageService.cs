﻿using AntimalnikAPI.BLL.Interfaces;
using AntimalnikAPI.Common;
using AntimalnikAPI.DAL;
using AntimalnikAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.BLL
{
    public class MessageService : IMessageService
    {
        private readonly AntimalnikDbContext _context;

        public MessageService(AntimalnikDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<List<Message>> GetSentMessages(string userName) => this._context.Messages.Where(x => x.Sender.UserName == userName).Include(x => x.Sender).Include(x => x.Reciever).ToListAsync();

        public Task<List<Message>> GetRecievedMessages(string userName) => this._context.Messages.Where(x => x.Reciever.UserName == userName).Include(x => x.Sender).Include(x => x.Reciever).ToListAsync();

        public async Task SendMessage(Message message)
        {
            this._context.Add(message);
            this._context.SaveChanges();
        }

        public async Task DeleteMessage(string id)
        {
            var message = this._context.Messages.SingleOrDefaultAsync(x => x.Id == id).Result;
            this._context.Remove(message);
            this._context.SaveChanges();
        }

        public async Task SendQuestion(Message message)
        {
            var admins = this._context.Users.Where(x => x.Role == RoleType.Admin).ToListAsync().Result;
            foreach (var admin in admins)
            {
                message.Reciever = admin;
                this._context.Add(message);
            }
            this._context.SaveChanges();
        }
    }
}
