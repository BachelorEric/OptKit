using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    [Serializable]
    public class DomainList<T> : List<T>, IDomainList where T : IDomain
    {
        public IList Deleted { get; }
    }
}
