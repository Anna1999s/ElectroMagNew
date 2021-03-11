using DomainModel.Account;
using DomainModel.Content;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Interfaces.Content
{
    public interface IChatService
    {
        Task<Message> SendMessage(Message entity);

        Task<IList<Message>> GetMessages(string UserId);

        Task<int> GetNewMessagesCount(string UserId);
    }
}
