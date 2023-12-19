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
    public class UserFriendRepository : EntityRepositoryAsync<UserFriend>, IUserFriendRepository
    {
        public UserFriendRepository(WhatsappBroContext context) : base(context)
        {

        }

        public async Task<List<UserFriend>> GetUserFriends(Expression<Func<UserFriend, bool>> filter=null)
        {
            return filter==null?await _entity.Include(x=>x.User).Include(x=>x.Friend).ToListAsync():await _entity.Where(filter).Include(x=>x.User).Include(x=>x.Friend).ToListAsync();
        }
    }
}
