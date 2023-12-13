using Application.Interfaces;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserDetailRepository:EntityRepositoryAsync<UserDetail>,IUserDetailRepository
    {
        public UserDetailRepository(WhatsappBroContext whatsappBroContext):base(whatsappBroContext)
        {

        }
    }
}
