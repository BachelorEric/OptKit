using OptKit.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using OptKit.Domain.Data;

namespace OptKit.Domain
{
    /// <summary>
    /// 实体
    /// </summary>
    [Serializable]
    public abstract class Entity : TrackableBase, IEntity, ITrackableState
    {
        IFieldData _fieldData;

        /// <summary>
        /// 字段数据,属性的值保存在
        /// </summary>
        protected IFieldData FieldData
        {
            get { return _fieldData ?? (_fieldData = new FieldData(PropertyContainer)); }
        }

        /// <summary>
        /// 属性容器
        /// </summary>
        public IPropertyContainer PropertyContainer
        {
            get { return _propertyContainer ?? (_propertyContainer = DomainManager.GetPropertyContainer(GetRawType())); }
        }

        #region ID

        object IEntity.Id
        {
            get => GetId();
            set => SetId(value);
        }

        /// <summary>
        /// 重写此方法获取实体主键<see cref="IEntity.Id"/>的值
        /// </summary>
        /// <returns></returns>
        protected abstract object GetId();

        /// <summary>
        /// 重写此方法设置实体主键<see cref="IEntity.Id"/>的值
        /// </summary>
        /// <param name="id"></param>
        protected abstract void SetId(object id);

        #endregion

        #region State

        TrackableState _state;
        /// <summary>
        /// 状态
        /// </summary>
        public TrackableState State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    stateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [NonSerialized]
        IPropertyContainer _propertyContainer;
        [NonSerialized]
        EventHandler stateChanged;

        /// <summary>
        /// 状态变更事件
        /// </summary>
        public event EventHandler StateChanged
        {
            add { stateChanged = (EventHandler)Delegate.Combine(stateChanged, value); }
            remove { stateChanged = (EventHandler)Delegate.Remove(stateChanged, value); }
        }

        /// <summary>
        /// 标记为新
        /// </summary>
        public void MarkNew()
        {
            State = TrackableState.New;
        }

        /// <summary>
        /// 标记为修改
        /// </summary>
        public void MarkModified()
        {
            State = TrackableState.Modified;
        }

        /// <summary>
        /// 标记为删除
        /// </summary>
        public void MarkDeleted()
        {
            State = TrackableState.Deleted;
        }

        /// <summary>
        /// 重置为未修改
        /// </summary>
        public void ResetState()
        {
            State = TrackableState.Unchanged;
        }

        #endregion

        public T Get<T>([CallerMemberName]string propertyName = null)
        {
            var property = PropertyContainer.Get(propertyName);
            return Get(property).ConvertTo<T>();
        }

        public object Get(string propertyName)
        {
            var property = PropertyContainer.Get(propertyName);
            return Get(property);
        }

        public object Get(IProperty property)
        {
            if (property is ICaculateProperty)
                return Get(property as ICaculateProperty);
            if (property is IListProperty)
                return Get(property as IListProperty);
            if (property is IRefEntityProperty)
                return Get(property as IRefEntityProperty);
            return FieldData.Get(property);
        }

        public T Get<T>(IProperty<T> property)
        {
            return Get((IProperty)property).ConvertTo<T>();
        }

        public object Get(IRefEntityProperty property)
        {
            if (FieldData.Exists(property))
                return FieldData.Get(property);
            if (FieldData.Exists(property.RefIdProperty))
            {
                var id = FieldData.Get(property.RefIdProperty);
                return RF.Find(property.PropertyType).GetById(id);
            }
            return null;
        }

        public T Get<T>(IRefEntityProperty<T> property)
        {
            return Get((IRefEntityProperty)property).ConvertTo<T>();
        }

        public object Get(IListProperty property)
        {
            if (FieldData.Exists(property))
                return FieldData.Get(property);
            object result = null;
            if (State == TrackableState.New)
                result = Activator.CreateInstance(property.PropertyType);
            else
                result = RF.Find(property.PropertyType).GetByParentId(GetId());
            FieldData.Set(property, result);
            return result;
        }

        public T Get<T>(IListProperty<T> property)
        {
            return Get((IListProperty)property).ConvertTo<T>();
        }

        public object Get(ICaculateProperty property)
        {
            return property.ValueProvider.Invoke(this);
        }

        public T Get<T>(ICaculateProperty<T> property)
        {
            return Get((ICaculateProperty)property).ConvertTo<T>();
        }

        public void Set(object value, [CallerMemberName]string propertyName = null)
        {
            var property = PropertyContainer.Get(propertyName);
            Set(value, property);
        }

        public void Set(object value, IProperty property)
        {
            if (property is ICaculateProperty)
                throw new AppException("Cannot set value to ICaculateProperty");
            if (property is IRefProperty)
                Set(value, property as IRefProperty);
            else
            {
                object oldValue = FieldData.Get(property);
                if (!object.Equals(oldValue, value))
                {
                    FieldData.Set(property, value);
                    OnPropertyChanged(property.PropertyName);
                    OnValueChanged(property.PropertyName, value, oldValue);
                    if (State == TrackableState.Unchanged)
                        MarkModified();
                }
            }
        }

        public void Set<T>(T value, IProperty<T> property)
        {
            Set(value, (IProperty)property);
        }

        public void Set<T>(T value, IRefProperty property)
        {
            object oldValue = FieldData.Get(property);
            if (!object.Equals(oldValue, value))
            {
                FieldData.Set(property, value);
                OnPropertyChanged(property.PropertyName);
                OnValueChanged(property.PropertyName, value, oldValue);
                if (property is IRefIdProperty)
                {
                    FieldData.Set(property.RefEntityProperty, null);
                    OnPropertyChanged(property.RefEntityProperty.PropertyName);
                }
                if (property is IRefEntityProperty)
                {
                    FieldData.Set(property.RefIdProperty, (value as Entity).GetId());
                    OnPropertyChanged(property.RefIdProperty.PropertyName);
                }
                if (State == TrackableState.Unchanged)
                    MarkModified();
            }
        }

        public void Set<T>(T value, IRefProperty<T> property)
        {
            Set<T>(value, (IRefProperty)property);
        }

        /// <summary>
        /// 创建<see cref="Entity"/>实例的代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public new static T Create<T>() where T : Entity
        {
            return DomainManager.Create<T>();
        }
    }
}
