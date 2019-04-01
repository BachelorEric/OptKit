using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 标记仓库使用的数据提供器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public class DataProviderAttribute : Attribute
    {
        public string ConnectionStringName { get; set; }

        public DataProviderAttribute() { }

        public DataProviderAttribute(string connectionStringName) { ConnectionStringName = connectionStringName; }
    }
}
