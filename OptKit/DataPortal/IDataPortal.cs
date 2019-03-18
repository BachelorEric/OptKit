using OptKit.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.DataPortal
{
    /// <summary>
    /// 数据门户
    /// </summary>
    [Service(typeof(ApiDataPortal))]
    public interface IDataPortal
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ApiResponse Execute(ApiRequest request);
    }
}
