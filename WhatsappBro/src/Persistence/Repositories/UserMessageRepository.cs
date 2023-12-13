using Application.Interfaces;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserMessageRepository:EntityRepositoryAsync<UserMessage>,IUserMessageRepository
    {
        public UserMessageRepository(WhatsappBroContext whatsappBroContext):base(whatsappBroContext)
        {
            
        }
    }
}
