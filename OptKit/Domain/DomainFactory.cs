using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 实体工厂
    /// </summary>
    public class DomainFactory
    {
        static ProxyGenerator _proxyGenerator = new ProxyGenerator();

        static IInterceptor Interceptor { get; set; }

        /// <summary>
        /// 实体拦截器
        /// </summary>
        /// <param name="interceptor"></param>
        public static void SetDomainInterceptor(IInterceptor interceptor)
        {
            Interceptor = interceptor;
        }

        /// <summary>
        /// 创建<see cref="Entity"/>实例的代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>() where T : Entity
        {
            return _proxyGenerator.CreateClassProxy<T>(Interceptor);
        }
    }
}
