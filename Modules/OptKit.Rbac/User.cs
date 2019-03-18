using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Rbac
{
    public class User : IEntity
    {
        public object Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
