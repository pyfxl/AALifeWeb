using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core.Domain
{
    public class UserQueryEvent
    {
        public UserQueryEvent(UserTable user)
        {
            this.User = user;
        }

        public UserTable User
        {
            get; private set;
        }
    }
}
