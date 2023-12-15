using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constants
{
    public static class ExceptionMessage
    {
        public static class UserFriend
        {
            public const string AlreadyFriend= "Böyle bir arkadaş zaten mevcut.";
        }
        public static class User
        {
            public const string AlreadyUserExist = "Böyle bir kullanıcı zaten mevcut.";
            public const string WrongPasswordOrUserName = "Kullanıcı adı veya parola yanlış.";
        }
    }
}
