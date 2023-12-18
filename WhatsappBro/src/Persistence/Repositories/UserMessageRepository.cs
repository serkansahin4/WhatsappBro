using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserMessageRepository : EntityRepositoryAsync<UserMessage>, IUserMessageRepository
    {
        public UserMessageRepository(WhatsappBroContext whatsappBroContext):base(whatsappBroContext)
        {
            
        }

        public async Task<List<UserMessage>> GetUserMessages(Expression<Func<UserMessage,bool>> filter)
        {
            return await _entity.Where(filter).Include(x => x.SenderUser).Include(x => x.ReceiverUser).ToListAsync();
        }
    }
}
