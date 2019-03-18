using OptKit.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OptKit.Configuration
{
    /// <summary>
    /// 键值对设置，键值对的读写是线程安全的
    /// </summary>
    public interface IConfigSection
    {
        /// <summary>
        /// 属性变更通知
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
		/// 获取子节点
		/// </summary>
        IConfigSection GetSection(string key);

        /// <summary>
		/// 设置子节点
		/// </summary>
        void SetSection(string key, IConfigSection section);

        /// <summary>
        /// 所有键
        /// </summary>
        IReadOnlyList<string> Keys { get; }

        /// <summary>
        /// 判断是否包含键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Contains(string key);

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string this[string key] { get; }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T Get<T>(string key, T defaultValue = default(T));

        /// <summary>
        /// 设置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set<T>(string key, T value);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        IReadOnlyList<T> GetList<T>(string key);

        /// <summary>
        /// 设置列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetList<T>(string key, IEnumerable<T> value);

        /// <summary>
        /// 移除值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="file"></param>
        void Save(FileName file);
    }
}
