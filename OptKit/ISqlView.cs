using OptKit.Domain.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit
{
    public interface ISqlView
    {
        IQuery GetSqlView();
    }
}
