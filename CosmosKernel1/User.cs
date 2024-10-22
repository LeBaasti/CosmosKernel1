using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelProject_One
{
    public class User
    {
        public string userName { get; private set; }
        public string password { get; private set; }
        public eUserLevel userLevel { get; private set; }

        public User(string uname, eUserLevel level)
        {
            userName = uname;
            userLevel = userLevel;
        }

        public void SetPassword(string newPassword) 
        {
            password = newPassword;
        }
    }

    public enum eUserLevel
    {
        kNone,
        kGuest,
        kUser,
        kAdministrator,
        LAST_INDEX
    }
}
