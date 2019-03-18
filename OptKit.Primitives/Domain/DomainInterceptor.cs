using Castle.DynamicProxy;
using Castle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptKit.Domain;

namespace OptKit.Primitives.Domain
{
    /// <summary>
    /// <see cref="Entity"/>的拦截器，实现属性变更通知
    /// </summary>
    class DomainInterceptor : IInterceptor
    {
        void IInterceptor.Intercept(IInvocation invocation)
        {
            //通过拦截器实现事件变更通知
            if (invocation.Method.IsPublic && invocation.Method.IsSpecialName && invocation.Method.Name.StartsWith("set_"))
            {
                var target = invocation.InvocationTarget as Entity;
                target.Set(invocation.Arguments[0], invocation.Method.Name.Substring(4));
            }
            else if (invocation.Method.IsPublic && invocation.Method.IsSpecialName && invocation.Method.Name.StartsWith("get_"))
            {
                var target = invocation.InvocationTarget as Entity;
                invocation.ReturnValue = target.Get(invocation.Method.Name.Substring(4));
            }
            else
                invocation.Proceed();
        }
    }
}
