using OptKit.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace OptKit.Configuration
{
    /// <summary>
    /// 配置节点
    /// </summary>
    public abstract class ConfigSection : IConfigSection, INotifyPropertyChanged, ICloneable
    {
        // Properties instances form a tree due to the nested properties containers.
        // All nodes in such a tree share the same syncRoot in order to simplify synchronization.
        // When an existing node is added to a tree, its syncRoot needs to change.
        protected object syncRoot;
        /// <summary>
        /// 父节点
        /// </summary>
        protected ConfigSection parent;
        /// <summary>
        /// 键值数据
        /// </summary>
        protected Dictionary<string, object> dict = new Dictionary<string, object>();

        /// <summary>
        /// 构造实例
        /// </summary>
        public ConfigSection()
        {
            this.syncRoot = new object();
        }

        /// <summary>
        /// 构造实例
        /// </summary>
        /// <param name="parent"></param>
        protected ConfigSection(ConfigSection parent)
        {
            this.parent = parent;
            this.syncRoot = parent.syncRoot;
        }

        /// <summary>
        /// 属性变更通知
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string key)
        {
            Volatile.Read(ref PropertyChanged)?.Invoke(this, new PropertyChangedEventArgs(key));
        }


        bool isDirty;
        /// <summary>
        /// Gets/Sets whether this properties container is dirty.
        /// IsDirty automatically gets set to <c>true</c> when a property in this container (or a nested container)
        /// changes.
        /// </summary>
        public bool IsDirty
        {
            get { return isDirty; }
            set
            {
                lock (syncRoot)
                {
                    if (value)
                        MakeDirty();
                    else
                        CleanDirty();
                }
            }
        }

        void MakeDirty()
        {
            // called within syncroot
            if (!isDirty)
            {
                isDirty = true;
                if (parent != null)
                    parent.MakeDirty();
            }
        }

        void CleanDirty()
        {
            if (isDirty)
            {
                isDirty = false;
                foreach (var section in dict.Values.OfType<ConfigSection>())
                {
                    section.CleanDirty();
                }
            }
        }

        /// <summary>
        /// Retrieves a string value from this Properties-container.
        /// Using this indexer is equivalent to calling <c>Get(key, string.Empty)</c>.
        /// </summary>
        public string this[string key]
        {
            get
            {
                lock (syncRoot)
                {
                    object val;
                    dict.TryGetValue(key, out val);
                    return val as string ?? string.Empty;
                }
            }
            set
            {
                Set(key, value);
            }
        }


        /// <summary>
        /// Gets the keys that are in use by this properties container.
        /// </summary>
        public IReadOnlyList<string> Keys
        {
            get
            {
                lock (syncRoot)
                {
                    return dict.Keys.ToArray();
                }
            }
        }

        /// <summary>
        /// Gets whether this properties instance contains any entry (value, list, or nested container)
        /// with the specified key.
        /// </summary>
        public bool Contains(string key)
        {
            lock (syncRoot)
            {
                return dict.ContainsKey(key);
            }
        }

        /// <summary>
        /// Creates a deep clone of this Properties container.
        /// </summary>
        public ConfigSection Clone()
        {
            lock (syncRoot)
            {
                return CloneWithParent(null);
            }
        }

        ConfigSection CloneWithParent(ConfigSection parent)
        {
            ConfigSection copy = Create(parent);
            foreach (var pair in dict)
            {
                ConfigSection child = pair.Value as ConfigSection;
                if (child != null)
                    copy.dict.Add(pair.Key, child.CloneWithParent(copy));
                else
                    copy.dict.Add(pair.Key, pair.Value);
            }
            return copy;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        /// <summary>
        /// Retrieves a single element from this Properties-container.
        /// </summary>
        /// <param name="key">Key of the item to retrieve</param>
        /// <param name="defaultValue">Default value to be returned if the key is not present.</param>
        public T Get<T>(string key, T defaultValue = default(T))
        {
            lock (syncRoot)
            {
                object val;
                if (dict.TryGetValue(key, out val))
                {
                    try
                    {
                        return Deserialize<T>(val);
                    }
                    catch (Exception ex)
                    {
                        RT.Logger.Warn(ex);
                        return defaultValue;
                    }
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        public IReadOnlyList<T> GetList<T>(string key)
        {
            lock (syncRoot)
            {
                object val;
                if (dict.TryGetValue(key, out val))
                {
                    return DeserializeList<T>(val, key);
                }
                return new T[0];
            }
        }

        public IConfigSection GetSection(string key)
        {
            bool isNewContainer = false;
            IConfigSection result;
            lock (syncRoot)
            {
                object oldValue;
                dict.TryGetValue(key, out oldValue);
                result = oldValue as IConfigSection;
                if (result == null)
                {
                    result = Create(this);
                    dict[key] = result;
                    isNewContainer = true;
                }
            }
            if (isNewContainer)
                OnPropertyChanged(key);
            return result;
        }

        public bool Remove(string key)
        {
            bool removed = false;
            lock (syncRoot)
            {
                object oldValue;
                if (dict.TryGetValue(key, out oldValue))
                {
                    removed = true;
                    HandleOldValue(oldValue);
                    MakeDirty();
                    dict.Remove(key);
                }
            }
            if (removed)
                OnPropertyChanged(key);
            return removed;
        }

        /// <summary>
        /// Sets a single element in this Properties-container.
        /// The element will be serialized using a TypeConverter if possible, or Json serializer otherwise.
        /// </summary>
        /// <remarks>Setting a key to <c>null</c> has the same effect as calling <see cref="Remove"/>.</remarks>
        public void Set<T>(string key, T value)
        {
            object serializedValue = Serialize<T>(value, key);
            SetSerializedValue(key, serializedValue);
        }

        public void SetList<T>(string key, IEnumerable<T> value)
        {
            if (value == null)
            {
                Remove(key);
                return;
            }
            T[] array = value.ToArray();
            if (array.Length == 0)
            {
                Remove(key);
                return;
            }
            object serializedValue = SerializeList<T>(array, key);
            SetSerializedValue(key, serializedValue);
        }

        public void SetSection(string key, IConfigSection section)
        {
            if (section == null)
            {
                Remove(key);
                return;
            }
            ConfigSection child = section as ConfigSection;
            lock (syncRoot)
            {
                for (ConfigSection ancestor = this; ancestor != null; ancestor = ancestor.parent)
                {
                    if (ancestor == child)
                        throw new InvalidOperationException("Cannot add a section container to itself.");
                }

                object oldValue;
                if (dict.TryGetValue(key, out oldValue))
                {
                    if (oldValue == child)
                        return;
                    HandleOldValue(oldValue);
                }
                lock (child.syncRoot)
                {
                    if (child.parent != null)
                        throw new InvalidOperationException("Cannot attach nested section that already have a parent.");
                    MakeDirty();
                    child.SetSyncRoot(syncRoot);
                    child.parent = this;
                    dict[key] = child;
                }
            }
            OnPropertyChanged(key);
        }

        void SetSyncRoot(object newSyncRoot)
        {
            this.syncRoot = newSyncRoot;
            foreach (var section in dict.Values.OfType<ConfigSection>())
            {
                section.SetSyncRoot(newSyncRoot);
            }
        }

        void SetSerializedValue(string key, object serializedValue)
        {
            if (serializedValue == null)
            {
                Remove(key);
                return;
            }
            lock (syncRoot)
            {
                object oldValue;
                if (dict.TryGetValue(key, out oldValue))
                {
                    if (object.Equals(serializedValue, oldValue))
                        return;
                    HandleOldValue(oldValue);
                }
                dict[key] = serializedValue;
            }
            OnPropertyChanged(key);
        }

        void HandleOldValue(object oldValue)
        {
            ConfigSection p = oldValue as ConfigSection;
            if (p != null)
            {
                Debug.Assert(p.parent == this);
                p.parent = null;
            }
        }

        public abstract void Save(FileName fileName);

        protected abstract ConfigSection Create(ConfigSection parent);

        protected abstract object Serialize<T>(object value, string key);

        protected abstract object SerializeList<T>(T[] value, string key);

        protected abstract T Deserialize<T>(object serializedVal);

        protected abstract IReadOnlyList<T> DeserializeList<T>(object value, string key);
    }
}
