using OptKit.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.Diagnostics
{
    /// <summary>
    /// 事件跟踪器，跟踪事件的执行耗时
    /// </summary>
    public class EventTracer
    {
        static List<EventTracerListener> Listeners = new List<EventTracerListener>();

        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="listener"></param>
        public static void AddListener(EventTracerListener listener)
        {
            Listeners.Add(listener);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="listener"></param>
        public static void RemoveListener(EventTracerListener listener)
        {
            Listeners.Remove(listener);
        }

        /// <summary>
        /// 写入事件
        /// </summary>
        /// <param name="caption">标题</param>
        /// <param name="context">上下文</param>
        /// <param name="elapsed">耗时（毫秒）</param>
        static void Write(string caption, object context, long elapsed)
        {
            for (int i = 0; i < Listeners.Count; i++)
            {
                Listeners[i].Write(caption, context, elapsed);
            }
        }

        /// <summary>
        /// 开始跟踪
        /// </summary>
        /// <param name="caption">目标</param>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public static IDisposable Start(string caption, object context)
        {
            return new EventTraceStopwatch(caption, context).Start();
        }

        [ThreadStatic]
        static string _scopeId;
        [ThreadStatic]
        static int _stackIndex;
        [ThreadStatic]
        static int _stackLevel;

        /// <summary>
        /// 事件堆的深度，同一线程中，事件嵌套的深度
        /// </summary>
        public static int StackLevel
        {
            get { return _stackLevel; }
            protected set { _stackLevel = value; }
        }

        /// <summary>
        /// 事件堆的顺序，记录事件的执行顺序
        /// </summary>
        public static int StackIndex
        {
            get { return _stackIndex; }
            protected set { _stackIndex = value; }
        }

        /// <summary>
        /// 同一线程中，事件嵌套的范围ID，用于跟踪事件的调用链
        /// </summary>
        public static string ScopeId
        {
            get { return _scopeId; }
            protected set { _scopeId = value; }
        }

        /// <summary>
        /// 事件跟踪计时器
        /// </summary>
        class EventTraceStopwatch : DisposableBase
        {
            [ThreadStatic]
            static int _stackLevel;
            [ThreadStatic]
            static string _scopeId;
            [ThreadStatic]
            static int _stackIndex;

            string _taregt;
            object _context;
            Stopwatch _sw = new Stopwatch();
            int _currentStackIndex;
            public bool Cancel { get; set; }

            public EventTraceStopwatch(string target, object context)
            {
                _taregt = target;
                _context = context;
            }

            public EventTraceStopwatch Start()
            {
                if (_stackLevel == 0)
                {
                    _stackIndex = 0;
                    _scopeId = Guid.NewGuid().ToString("N");
                }
                _currentStackIndex = ++_stackIndex;
                _stackLevel++;
                _sw.Start();
                return this;
            }

            public void Stop()
            {
                try
                {
                    _sw.Stop();
                    EventTracer.StackIndex = _currentStackIndex;
                    EventTracer.ScopeId = _scopeId;
                    EventTracer.StackLevel = _stackLevel;
                    if (!Cancel)
                        EventTracer.Write(_taregt, _context, _sw.ElapsedMilliseconds);
                }
                finally
                {
                    _stackLevel--;
                }
            }

            protected override void Cleanup(bool disposing)
            {
                Stop();
            }
        }
    }
}
