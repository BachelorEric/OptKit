using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace OptKit.DataPortal
{
    /// <summary>
    /// API请求
    /// </summary>
    public class ApiRequest
    {
        /// <summary>
        /// API服务的类型。可使用类名缩写，不区分大小写，例如：entityService;或者使用类全名，区分大小写，例如OptKit.Domain.EntityService,OptKit
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 所有的参数。参数需要按顺序赋值。
        /// </summary>
        public object[] Arguments { get; set; }

        /// <summary>
        /// 参数类型列表
        /// </summary>
        public string[] ArgumentTypes { get; set; }

        /// <summary>
        /// 方法名称。不区分大小写。
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 泛型方法的泛参类型
        /// </summary>
        public string[] MethodGenericArguments { get; set; }

        /// <summary>
        /// 上下文，从客户端传到服务器端的上下文。例如库存组织，身份票据等
        /// </summary>
        public HybridDictionary Context { get; set; } = new HybridDictionary();
    }
}
