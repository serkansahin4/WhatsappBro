using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Mappings
{
    public class UserFriendMap : IEntityTypeConfiguration<UserFriend>
    {
        public void Configure(EntityTypeBuilder<UserFriend> builder)
        {
            builder.HasKey(x => new { x.UserId, x.FriendId });

            builder.HasOne(x => x.User).WithMany(x => x.UserFriends).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Friend).WithMany().HasForeignKey(x => x.FriendId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
