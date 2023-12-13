using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class WhatsappBroContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-36N2808;Database=WhatsappBro;Trusted_Connection=true;");
            base.OnConfiguring(optionsBuilder);
        }
        //public WhatsappBroContext(DbContextOptions<WhatsappBroContext> options) : base(options)
        //{

        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserDetailMap());
            builder.ApplyConfiguration(new UserFriendMap());
            builder.ApplyConfiguration(new UserMessageMap());
            base.OnModelCreating(builder);
        }

        public virtual DbSet<UserFriend> UserFriends { get; set; }
        public virtual DbSet<UserMessage> UserMessages { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
    }
}
