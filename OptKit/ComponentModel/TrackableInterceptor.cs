using Castle.DynamicProxy;
using Castle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.ComponentModel
{
    /// <summary>
    /// <see cref="TrackableBase"/>的拦截器，实现属性变更通知
    /// </summary>
    internal class TrackableInterceptor : IInterceptor
    {
        /// <summary>
        /// 拦截器实体
        /// </summary>
        public static TrackableInterceptor Interceptor = new TrackableInterceptor();

        void IInterceptor.Intercept(IInvocation invocation)
        {
            System.Diagnostics.Debug.WriteLine(invocation.Method.Name);
            //通过拦截器实现事件变更通知
            if (invocation.Method.IsPublic && invocation.Method.IsSpecialName && invocation.Method.Name.StartsWith("set_"))
            {
                var property = invocation.Method.Name.Substring(4);
                var target = invocation.InvocationTarget as TrackableBase;
                var oldValue = OptKit.Reflection.TypeDescriptor.GetValue(target, property);
                var newValue = invocation.Arguments[0];
                invocation.Proceed();
                if (!object.Equals(oldValue, newValue))
                {
                    target.RaisePropertyChanged(property);
                    target.RaiseValueChanged(property, newValue, oldValue);
                }
            }
            else
                invocation.Proceed();
        }
    }
}
