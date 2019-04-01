using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit.Modules
{
    /// <summary>
    /// 模块信息声明
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ModuleInfoAttribute : Attribute
    {
        /// <summary>
        /// 模块类型
        /// </summary>
        public Type ModuleType { get; set; }

        /// <summary>
        /// 加载顺序
        /// </summary>
        public int Level { get; set; } = ModuleLevel.Main;

        /// <summary>
        /// 构造<see cref="ModuleInfoAttribute"/>实例
        /// </summary>
        public ModuleInfoAttribute() { }

        /// <summary>
        /// 构造<see cref="ModuleInfoAttribute"/>实例
        /// </summary>
        /// <param name="moduleType">模块类型</param>
        public ModuleInfoAttribute(Type moduleType)
        {
            ModuleType = moduleType;
        }
    }
}
