using AntimalnikAPI.DAL.Models;
using AntimalnikAPI.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AntimalnikAPI.DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AntimalnikDbContext _context;

        public MessageRepository(AntimalnikDbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Message> GetMessages() => this._context.Messages.ToList();

        public List<Message> GetMessages(Expression<Func<Message, bool>> predicate) => this._context.Messages.Where(predicate).ToList();

        public Message GetMessage(string id) => this._context.Messages.SingleOrDefault(x => x.Id == id);

        public Message GetMessage(Expression<Func<Message, bool>> predicate) => this._context.Messages.SingleOrDefault(predicate);

        public void AddMessage(Message message)
        {
            this._context.Messages.Add(message);
            this._context.SaveChanges();
        }

        public void UpdateMessage(Message message)
        {
            this._context.Messages.Update(message);
            this._context.SaveChanges();
        }

        public void RemoveMessage(Message message)
        {
            this._context.Messages.Remove(message);
            this._context.SaveChanges();
        }
    }
}
