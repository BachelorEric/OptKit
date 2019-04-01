using OptKit.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OptKit.Runtime
{
    partial class AppRuntime
    {
        /// <summary>
        /// 模块程序集过滤器
        /// </summary>
        public static Func<string, bool> ModuleAssemblyFilter { get; set; }

        private static object _moduleLock = new object();
        private static IList<ModuleAssembly> _modules;

        /// <summary>
        /// 获取当前环境被初始化的所有模块。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ModuleAssembly> GetModules()
        {
            if (_modules == null)
            {
                lock (_moduleLock)
                {
                    if (_modules == null)
                    {
                        var assemblies = new List<Assembly> { typeof(RT).Assembly };
                        assemblies.AddRange(GetDlls().Where(p => ModuleAssemblyFilter == null || ModuleAssemblyFilter(p)).Select(p => Assembly.LoadFrom(p)));
                        _modules = LoadSortedModules(assemblies);
                    }
                }
            }
            return _modules;
        }

        static IEnumerable<string> GetDlls()
        {
            foreach (var dll in Directory.GetFiles(Environment.DllRootDirectory, "OptKit.*.dll", SearchOption.TopDirectoryOnly))
                yield return dll;
            var m = Path.Combine(Environment.DllRootDirectory, "Modules");
            if (Directory.Exists(m))
                foreach (var dll in Directory.GetFiles(m, "*.dll", SearchOption.TopDirectoryOnly))
                    yield return dll;
            var ui = Path.Combine(Environment.DllRootDirectory, "UI");
            if (Directory.Exists(ui))
                foreach (var dll in Directory.GetFiles(ui, "*.dll", SearchOption.TopDirectoryOnly))
                    yield return dll;
        }

        static List<ModuleAssembly> LoadSortedModules(IEnumerable<Assembly> assemblies)
        {
            var list = new List<ModuleAssembly>();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var attribute = assembly.GetCustomAttribute<ModuleInfoAttribute>();
                    if (attribute != null)
                    {
                        list.Add(new ModuleAssembly(assembly, attribute.Level, attribute.ModuleType));
                    }
                }
                catch (ReflectionTypeLoadException exc)
                {
                    string message = assembly.FullName + exc.LoaderExceptions.Select(p => p.Message).Join("\r\n");
                    throw new SystemException(message, exc);
                }
                catch (Exception exc)
                {
                    throw new SystemException(assembly.FullName, exc);
                }
            }

            //将 list 中集合中的元素，先按照 ModuleIndex 排序；
            //然后同一个启动级别中的模块，再按照引用关系来排序。
            var sorted = new List<ModuleAssembly>(list.Count);
            var index = 0;
            var sortedItems = SortByReference(list.OrderBy(p => p.Level));
            foreach (var item in sortedItems)
            {
                item.Index = index++;
                sorted.Add(item);
            }

            return sorted;
        }

        static List<ModuleAssembly> SortByReference(IEnumerable<ModuleAssembly> list)
        {
            //items 表示待处理列表。
            var items = list.ToList();
            var sorted = new List<ModuleAssembly>(items.Count);

            while (items.Count > 0)
            {
                for (int i = 0, c = items.Count; i < c; i++)
                {
                    var item = items[i];
                    bool referencesOther = false;
                    var refItems = item.Assembly.GetReferencedAssemblies().ToDictionary(p => p.FullName);
                    for (int j = 0, c2 = items.Count; j < c2; j++)
                    {
                        if (i != j)
                        {
                            if (refItems.ContainsKey(items[j].Assembly.FullName))
                            {
                                referencesOther = true;
                                break;
                            }
                        }
                    }
                    //没有被任何一个程序集引用，则把这个加入到结果列表中，并从待处理列表中删除。
                    if (!referencesOther)
                    {
                        sorted.Add(item);
                        items.RemoveAt(i);

                        //跳出循环，从新开始。
                        break;
                    }
                }
            }

            return sorted;
        }
    }
}
