using AntimalnikAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AntimalnikAPI.DAL.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        List<Message> GetMessages();
        List<Message> GetMessages(Expression<Func<Message, bool>> predicate);
        Message GetMessage(string id);
        Message GetMessage(Expression<Func<Message, bool>> predicate);
        void AddMessage(Message message);
        void UpdateMessage(Message message);
        void RemoveMessage(Message message);
    }
}
