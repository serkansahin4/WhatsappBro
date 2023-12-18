using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserMessageRepository:IEntityRepositoryAsync<UserMessage>
    {
        Task<List<UserMessage>> GetUserMessages(Expression<Func<UserMessage, bool>> filter);
    }
}
