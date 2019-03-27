using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace OptKit.Domain
{
    public class DomainManager
    {
        static ProxyGenerator _proxyGenerator = new ProxyGenerator();

        static IInterceptor Interceptor { get; set; } = new DomainInterceptor();

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

        /// <summary>
        /// 获取属性容器
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static IPropertyContainer GetPropertyContainer(Type entityType)
        {
            var register = PropertyRegisterContainer.GetOrCreateRegisterContainer(entityType);
            return register.Container;
        }

        /// <summary>
        /// 注册属性
        /// </summary>
        /// <param name="property"></param>
        public static void RegisterProperty(Property property)
        {
            Check.NotNull(property, nameof(property));
            PropertyRegisterContainer.RegisterProperty(property);
        }

        /// <summary>
        /// 注册扩展属性
        /// </summary>
        internal static void RegisterExtensionProperties()
        {
            foreach (var module in RT.GetModules())
            {
                foreach (var declare in module.Assembly.GetCustomAttributes<PropertyDeclareAttribute>())
                {
                    PropertyRegisterContainer.RunPropertyResigtry(declare.DeclareType);
                }
            }
        }
    }
}
