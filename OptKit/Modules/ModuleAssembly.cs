﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OptKit.Modules
{
    /// <summary>
    /// 表示某一个模块程序集
    /// </summary>
    public class ModuleAssembly
    {
        public ModuleAssembly(Assembly assembly, int level, Type moduleType)
        {
            ModuleType = moduleType;
            Level = level;
            Assembly = assembly;
        }

        /// <summary>
        /// 程序集当中的模块定义类型，实现<see cref="IModule"/>的类型
        /// </summary>
        public Type ModuleType { get; }

        /// <summary>
        /// 程序集本身
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// 模块声明的顺序
        /// </summary>
        internal int Level { get; }

        /// <summary>
        /// 本属性表示模块在所有模块中的启动索引号。
        /// 索引号表示了模块的启动优先级，索引号越小，越先被启动。
        /// 该优先级的计算方式为：
        /// 
        /// 1. 所有 ServiceModule 的索引号全部少于所有的 UIModule 的索引号；
        /// 2. 接着按照 ModuleIndex 进行排序，越小的 ModuleIndex 对应越小的索引号。
        /// 3. 对于 ModuleIndex 相同的模块，则根据引用关系对模块进行排序，引用其它模块越少的模块，对应的索引号更小。
        /// </summary>
        public int Index { get; internal set; }
    }
}