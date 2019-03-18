using OptKit.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Rbac
{
    public class UserService : RemoteService
    {
        public virtual User Find(string name)
        {
            return new User { Name = name };
        }
    }
}
