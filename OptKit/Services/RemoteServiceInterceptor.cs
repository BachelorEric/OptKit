using Castle.DynamicProxy;
using OptKit.DataPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OptKit.Services
{
    public class RemoteServiceInterceptor : IInterceptor
    {
        static int _apiTimeoutMillisecond = RT.Config.Get("ApiTimeoutMinutes", 15) * 60000;

        public void Intercept(IInvocation invocation)
        {
            if (RT.DataPortalMode == DataPortal.DataPortalMode.Local || invocation.TargetType.IsDefined(typeof(LocalAttribute)) || invocation.Method.IsDefined(typeof(LocalAttribute)))
            {
                invocation.Proceed();
                return;
            }

            var client = new ApiClient();
            var argementTypes = invocation.Method.GetParameters().Select(p => p.ParameterType).ToArray();
            if (invocation.Method.ReturnType == typeof(void))
                client.Execute(invocation.TargetType.GetQualifiedName(), invocation.Method.Name, invocation.GenericArguments, argementTypes, invocation.Arguments, _apiTimeoutMillisecond);
            else
                invocation.ReturnValue = client.Execute(invocation.Method.ReturnType, invocation.TargetType.GetQualifiedName(), invocation.Method.Name, invocation.GenericArguments, argementTypes, invocation.Arguments, _apiTimeoutMillisecond);
        }
    }
}
