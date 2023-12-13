using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection serviceDescriptors,IConfiguration configuration)
        {
            serviceDescriptors.AddTransient<IUserFriendRepository, UserFriendRepository>();
            serviceDescriptors.AddTransient<IUserMessageRepository, UserMessageRepository>();
            serviceDescriptors.AddTransient<IUserDetailRepository, UserDetailRepository>();

            serviceDescriptors.AddIdentityCore<AppUser>().AddEntityFrameworkStores<WhatsappBroContext>();
            serviceDescriptors.AddDbContext<WhatsappBroContext>(x=>x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
