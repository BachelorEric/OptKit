using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Data
{
    public interface IDbRepository
    {
        DbSetting DbSetting { get; }
    }
}
