﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserFriend : IEntity
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
