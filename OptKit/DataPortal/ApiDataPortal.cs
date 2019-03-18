using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OptKit.DataPortal
{
    class ApiDataPortal : IDataPortal
    {
        public ApiResponse Execute(ApiRequest request)
        {
            //TODO 身份验证
            ApiResponse response = new ApiResponse();
            response.Success = true;
            Type type = Type.GetType(request.ServiceName);
            var instance = Activator.CreateInstance(type);
            MethodInfo method = null;
            if (request.MethodGenericArguments.IsNotEmpty())
            {
                var methods = type.GetMember(request.Method, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Public).OfType<MethodInfo>();
                foreach (var m in methods)
                {
                    if (m.IsGenericMethod)
                    {
                        var args = m.GetGenericArguments();
                        if(args.Length == request.MethodGenericArguments.Length)
                        {
                            method = m.MakeGenericMethod(request.MethodGenericArguments.Select(p => GetType(p)).ToArray());
                            break;
                        }
                    }
                }
            }
            else
            {
                method = type.GetMethod(request.Method, request.ArgumentTypes.Select(p => GetType(p)).ToArray());
            }

            response.Data = method.Invoke(instance, request.Arguments);
            return response;
        }

        Type GetType(string typeName)
        {
            bool isNullable = typeName.EndsWith("?");
            if (isNullable)
            {
                typeName = typeName.TrimEnd('?');
                var type = GetType(typeName);
                return typeof(Nullable<>).MakeGenericType(type);
            }
            TypeCode code;
            if (Enum.TryParse(typeName, out code))
            {
                switch (code)
                {
                    case TypeCode.Boolean: return typeof(Boolean);
                    case TypeCode.Byte: return typeof(Byte);
                    case TypeCode.Char: return typeof(Char);
                    case TypeCode.DateTime: return typeof(DateTime);
                    case TypeCode.DBNull: return typeof(DBNull);
                    case TypeCode.Decimal: return typeof(Decimal);
                    case TypeCode.Double: return typeof(Double);
                    case TypeCode.Int16: return typeof(Int16);
                    case TypeCode.Int32: return typeof(Int32);
                    case TypeCode.Int64: return typeof(Int64);
                    case TypeCode.SByte: return typeof(SByte);
                    case TypeCode.Single: return typeof(Single);
                    case TypeCode.String: return typeof(String);
                    case TypeCode.UInt16: return typeof(UInt16);
                    case TypeCode.UInt32: return typeof(UInt32);
                    case TypeCode.UInt64: return typeof(UInt64);
                }
            }
            return Type.GetType(typeName);
        }
    }
}
