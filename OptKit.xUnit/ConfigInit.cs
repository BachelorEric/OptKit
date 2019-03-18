using OptKit.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OptKit.xUnit
{
    public class ConfigInit
    {
        public ConfigInit()
        {
            ConfigManager.Create().UserJsonConfig("appsettings.json");
        }
    }
}
