using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace OptKit.DataPortal
{
    /// <summary>
    /// API响应
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 如果失败，消息包含失败的信息，如果成功则为 null
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 如果成功，返回API方法的结果，如果失败则为 null
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 上下文，从服务器返回的上下文。例如身份票据。
        /// </summary>
        public HybridDictionary Context { get; set; } = new HybridDictionary();
    }
}

