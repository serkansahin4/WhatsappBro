using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Mappings
{
    public class UserDetailMap : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.User).WithOne(x => x.UserDetail).HasForeignKey<UserDetail>(x=>x.Id).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
