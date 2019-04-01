using OptKit.ComponentModel;
using OptKit.Domain.Data;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace OptKit.Domain
{
    /// <summary>
    /// 领域对象
    /// </summary>
    [Serializable]
    public abstract class DomainObject : TrackableBase, IDomain, ITrackableState
    {
        IFieldData _fieldData;
        [NonSerialized]
        IPropertyContainer _propertyContainer;
        [NonSerialized]
        EventHandler _stateChanged;

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
                    _stateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 状态变更事件
        /// </summary>
        public event EventHandler StateChanged
        {
            add { _stateChanged = (EventHandler)Delegate.Combine(_stateChanged, value); }
            remove { _stateChanged = (EventHandler)Delegate.Remove(_stateChanged, value); }
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
            return FieldData.Get(property).ConvertTo(property.PropertyType);
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
            if (State == TrackableState.New || !(this is IEntity))
                result = Activator.CreateInstance(property.PropertyType);
            else
                result = RF.Find(property.PropertyType).GetByParentId((this as IEntity)?.Id);
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
                    OnPropertyChanged(property.Name);
                    OnValueChanged(property.Name, value, oldValue);
                    if (State == TrackableState.Unchanged)
                        MarkModified();
                }
            }
        }

        public void Set(object value, IRefProperty property)
        {
            object oldValue = FieldData.Get(property);
            if (!object.Equals(oldValue, value))
            {
                FieldData.Set(property, value);
                OnPropertyChanged(property.Name);
                OnValueChanged(property.Name, value, oldValue);
                if (property is IRefIdProperty)
                {
                    FieldData.Set(property.RefEntityProperty, null);
                    OnPropertyChanged(property.RefEntityProperty.Name);
                }
                if (property is IRefEntityProperty)
                {
                    FieldData.Set(property.RefIdProperty, (value as IEntity)?.Id);
                    OnPropertyChanged(property.RefIdProperty.Name);
                }
                if (State == TrackableState.Unchanged)
                    MarkModified();
            }
        }

        public void Load(object value, IProperty property)
        {
            FieldData.Set(property, value);
            if (property is IRefIdProperty)
                FieldData.Set(((IRefIdProperty)property).RefEntityProperty, null);
            else if (property is IRefEntityProperty)
                FieldData.Set(((IRefEntityProperty)property).RefIdProperty, (value as IEntity)?.Id);
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
