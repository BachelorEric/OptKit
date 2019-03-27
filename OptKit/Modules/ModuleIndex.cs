using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Modules
{
    /// <summary>
    /// 模块加载顺序
    /// </summary>
    public class ModuleIndex
    {
        /// <summary>
        /// 框架系统模块
        /// </summary>
        public const int System = -1;

        /// <summary>
        /// 主业务模块
        /// </summary>
        public const int Main = 100;

        /// <summary>
        /// 扩展业务模块
        /// </summary>
        public const int Extension = 200;

        /// <summary>
        /// 客制业务模块
        /// </summary>
        public const int Customized = 300;
    }
}
