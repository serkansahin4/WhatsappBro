using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFriendFeatures.InsertUserFriend
{
    public class InsertUserFriendCommand:IRequest
    {
        public string UserName { get; set; }
        public Guid FriendId { get; set; }
    }
}
