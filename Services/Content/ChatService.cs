using DomainModel.Account;
using DomainModel.Content;
using Interfaces.Content;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Content
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _dbContext;

        public ChatService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Message> SendMessage(Message entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IList<Message>> GetMessages(string UserId)
        {
            var messages = await _dbContext.Messages.Where(_ => _.SenderUserId == UserId || _.RecipientUserId == UserId).ToListAsync();
            return messages;
        }

        public async Task<int> GetNewMessagesCount(string UserId)
        {
            var newMessages = await _dbContext.Messages.Where(_ => _.RecipientUserId == UserId && _.Read == false).CountAsync();
            return newMessages;
        }
    }
}
