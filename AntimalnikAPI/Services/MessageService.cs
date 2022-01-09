using AntimalnikAPI.Data;
using AntimalnikAPI.Models;
using AntimalnikAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        public MessageService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public Task<List<Message>> GetSentMessages(string userName) => this._context.Messages.Where(x => x.Sender.UserName == userName).ToListAsync();

        public Task<List<Message>> GetRecievedMessages(string userName) => this._context.Messages.Where(x => x.Reciever.UserName == userName).ToListAsync();

        public async Task SendMessage(Message message)
        {
            this._context.Add(message);
            await this._context.SaveChangesAsync();
        }

        public async Task DeleteMessage(Message message)
        {
            this._context.Remove(message);
            await this._context.SaveChangesAsync();
        }
    }
}
