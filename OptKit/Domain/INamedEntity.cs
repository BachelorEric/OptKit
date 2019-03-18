using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    public interface INamedEntity : IEntity
    {
        string Name { get; set; }
    }
}
