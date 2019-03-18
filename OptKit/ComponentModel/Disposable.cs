using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptKit.ComponentModel
{
    /// <summary>
    /// 可回收的基类
    /// </summary>
    public class DisposableBase : IDisposable
    {
        private bool disposed;

        /// <summary>
        /// 是否已回收
        /// </summary>
        protected bool Disposed
        {
            get
            {
                lock (this)
                {
                    return disposed;
                }
            }
        }

        #region IDisposable Members


        /// <summary>
        /// 回收
        /// </summary>
        public void Dispose()
        {
            lock (this)
            {
                if (disposed == false)
                {
                    Cleanup(true);
                    disposed = true;

                    GC.SuppressFinalize(this);
                }
            }
        }

        #endregion

        /// <summary>
        /// 清理
        /// </summary>
        protected virtual void Cleanup(bool disposing)
        {
            // override to provide cleanup
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~DisposableBase()
        {
            Cleanup(false);
        }
    }

    public class Disposable : DisposableBase
    {
        public static readonly Disposable Empty = new Disposable();

        public static IDisposable Create(Action cleanup)
        {
            return new Disposable { _cleanup = cleanup };
        }
        Action _cleanup;

        protected override void Cleanup(bool disposing)
        {
            base.Cleanup(disposing);
            if (disposing)
                _cleanup?.Invoke();
        }
    }
}
