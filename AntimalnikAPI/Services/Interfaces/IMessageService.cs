using AntimalnikAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.Services.Interfaces
{
    public interface IMessageService
    {
        Task<List<Message>> GetSentMessages(string userName);
        Task<List<Message>> GetRecievedMessages(string userName);
        Task SendMessage(Message message);
        Task DeleteMessage(string id);
        Task SendQuestion(Message message);
    }
}
