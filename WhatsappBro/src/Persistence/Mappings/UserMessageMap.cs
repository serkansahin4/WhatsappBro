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
    public class UserMessageMap : IEntityTypeConfiguration<UserMessage>
    {
        public void Configure(EntityTypeBuilder<UserMessage> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.Message).HasMaxLength(3000);

            builder.HasOne(x => x.SenderUser).WithMany(x => x.UserMessages).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ReceiverUser).WithMany().HasForeignKey(x => x.ReceiverId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
