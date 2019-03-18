using OptKit.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 属性容器工厂
    /// </summary>
    public abstract class PropertyContainerFactory
    {
        static PropertyContainerFactory _instance;

        public abstract IPropertyContainer Get(Type type);

        /// <summary>
        /// 实例
        /// </summary>
        public static PropertyContainerFactory Instance
        {
            get
            {
                if (_instance == null)
                    throw new AppException(Resources.TypeNotInitialized.FormatArgs(nameof(PropertyContainerFactory)));
                return _instance;
            }
        }

        /// <summary>
        /// 设置工厂
        /// </summary>
        /// <param name="factory"></param>
        public static void SetFactory(PropertyContainerFactory factory)
        {
            _instance = factory;
        }
    }
}
