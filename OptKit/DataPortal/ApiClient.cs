using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace OptKit.DataPortal
{
    class ApiClient
    {
        static string Ticket;

        /// <summary>
        /// 执行远程API服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceName">服务名称</param>
        /// <param name="method">方法名</param>
        /// <param name="genericArguments">方法泛参</param>
        /// <param name="argumentTypes">参数类型</param>
        /// <param name="arguments">参数</param>
        /// <param name="timeout">毫秒</param>
        /// <returns></returns>
        public T Execute<T>(string serviceName, string method, Type[] genericArguments, Type[] argumentTypes, object[] arguments, int? timeout)
        {
            var result = Post(serviceName, method, genericArguments, argumentTypes, arguments, timeout);
            var response = JsonConvert.DeserializeObject<ApiResponse>(result);
            var ticket = response.Context["Ticket"]?.ToString();
            if (ticket.IsNotEmpty())
                Ticket = ticket;
            if (response.Success)
                return response.Data.ConvertTo<T>();
            throw new Exception(response.Message);
        }

        public void Execute(string serviceName, string method, Type[] genericArguments, Type[] argumentTypes, object[] arguments, int? timeout)
        {
            var result = Post(serviceName, method, genericArguments, argumentTypes, arguments, timeout);
            var response = JsonConvert.DeserializeObject<ApiResponse>(result);
            var ticket = response.Context["Ticket"]?.ToString();
            if (ticket.IsNotEmpty())
                Ticket = ticket;
            if (!response.Success)
                throw new Exception(response.Message);
        }

        /// <summary>
        /// 执行远程API服务
        /// </summary>
        /// <param name="returnType"></param>
        /// <param name="serviceName"></param>
        /// <param name="method"></param>
        /// <param name="genericArguments"></param>
        /// <param name="argumentTypes"></param>
        /// <param name="arguments"></param>
        /// <param name="timeout">毫秒</param>
        /// <returns></returns>
        public object Execute(Type returnType, string serviceName, string method, Type[] genericArguments, Type[] argumentTypes, object[] arguments, int? timeout)
        {
            var result = Post(serviceName, method, genericArguments, argumentTypes, arguments, timeout);
            var response = JsonConvert.DeserializeObject<ApiResponse>(result);
            var ticket = response.Context["Ticket"]?.ToString();
            if (ticket.IsNotEmpty())
                Ticket = ticket;
            if (response.Success)
                return response.Data.ConvertTo(returnType);
            throw new Exception(response.Message);
        }

        string Post(string serivceName, string method, Type[] genericArguments, Type[] argumentTypes, object[] arguments, int? timeout)
        {
            var request = new ApiRequest
            {
                ServiceName = serivceName,
                Method = method,
                MethodGenericArguments = genericArguments?.Select(p => GetTypeName(p)).ToArray(),
                ArgumentTypes = argumentTypes.Select(p => GetTypeName(p)).ToArray(),
                Arguments = arguments
            };
            request.Context["Ticket"] = Ticket;
            request.Context["TenantId"] = RT.TenantId;
            var data = JsonConvert.SerializeObject(request);
            byte[] postData = Encoding.UTF8.GetBytes(data);
            WebClient client = new ApiWebClient(timeout);
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            byte[] responseData = client.UploadData(RT.Config.Get<string>("ApiUrl"), "POST", postData);
            return Encoding.UTF8.GetString(responseData);
        }

        string GetTypeName(Type type)
        {
            bool isNullabel = type.IsNullable();
            if (isNullabel)
                type = type.IgnoreNullable();
            var typeCode = Type.GetTypeCode(type);
            var typeName = "";
            if (typeCode != TypeCode.Object && typeCode != TypeCode.Empty)
                typeName = typeCode.ToString();
            else
                typeName = type.GetQualifiedName();
            if (isNullabel)
                typeName += "?";
            return typeName;
        }

        class ApiWebClient : WebClient
        {
            /// <summary>
            /// 超时时间(毫秒)
            /// </summary>
            public int? Timeout { get; set; }

            public ApiWebClient()
            {
            }

            public ApiWebClient(int? timeout)
            {
                Timeout = timeout;
            }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var result = base.GetWebRequest(address);
                if (Timeout.HasValue)
                    result.Timeout = Timeout.Value;
                return result;
            }
        }
    }
}
