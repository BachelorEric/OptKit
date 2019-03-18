using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Configuration
{
    [Serializable]
    public class ConnectionStringSection
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
    }
}
