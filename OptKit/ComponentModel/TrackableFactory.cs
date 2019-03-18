using Castle.Core;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.ComponentModel
{
    /// <summary>
    /// 可追踪对象的工厂，创建对象时会通过拦截器实现AOP，创建对象的动态代理
    /// </summary>
    public class TrackableFactory
    {
        static ProxyGenerator _proxyGenerator = new ProxyGenerator();

        /// <summary>
        /// 创建<see cref="TrackableBase"/>实例的代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>() where T : TrackableBase
        {
            return _proxyGenerator.CreateClassProxy<T>(TrackableInterceptor.Interceptor);
        }

        /// <summary>
        /// 获取类型代理前的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetRawType(Type type)
        {
            while (ProxyServices.IsDynamicProxy(type))
                type = type.BaseType;
            return type;
        }
    }
}
