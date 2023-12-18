using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
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
            serviceDescriptors.AddScoped<IUserFriendRepository, UserFriendRepository>();
            serviceDescriptors.AddScoped<IUserMessageRepository, UserMessageRepository>();
            serviceDescriptors.AddScoped<IUserDetailRepository, UserDetailRepository>();

            serviceDescriptors.AddSignalR();
            serviceDescriptors.AddDbContext<WhatsappBroContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            serviceDescriptors.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<WhatsappBroContext>();
            
        }
    }
}
