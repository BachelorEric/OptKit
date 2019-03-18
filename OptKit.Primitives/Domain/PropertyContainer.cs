using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Primitives.Domain
{
    class PropertyContainer : IPropertyContainer
    {
        public Type OwnerType => throw new NotImplementedException();

        public IReadOnlyList<IProperty> Properties => throw new NotImplementedException();

        public IProperty Find(string proprtyName, bool ignoreCase = false)
        {
            throw new NotImplementedException();
        }
    }
}
