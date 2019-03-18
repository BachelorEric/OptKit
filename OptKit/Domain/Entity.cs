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
            get { return _fieldData ?? (_fieldData = DataFactory.Instance.Create(GetType())); }
        }

        object IEntity.Id
        {
            get => GetId();
            set => SetId(value);
        }
        TrackableState _state;
        public TrackableState State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    StateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
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

        [NonSerialized]
        IPropertyContainer _propertyContainer;

        public event EventHandler StateChanged;

        /// <summary>
        /// 属性容器
        /// </summary>
        public IPropertyContainer PropertyContainer
        {
            get { return _propertyContainer ?? (_propertyContainer = PropertyContainerFactory.Instance.Get(GetType())); }
        }

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
            return _fieldData.Get(property);
        }

        public T Get<T>(IProperty<T> property)
        {
            return Get((IProperty)property).ConvertTo<T>();
        }

        public object Get(IRefEntityProperty property)
        {
            if (_fieldData.Exists(property))
                return _fieldData.Get(property);
            if (_fieldData.Exists(property.RefIdProperty))
            {
                var id = _fieldData.Get(property.RefIdProperty);
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
            if (_fieldData.Exists(property))
                return _fieldData.Get(property);
            object result = null;
            if (State == TrackableState.New)
                result = Activator.CreateInstance(property.PropertyType);
            else
                result = RF.Find(property.PropertyType).GetByParentId(GetId());
            _fieldData.Set(property, result);
            return result;
        }

        public T Get<T>(IListProperty<T> property)
        {
            return Get((IListProperty)property).ConvertTo<T>();
        }

        public object Get(ICaculateProperty property)
        {
            return property.ValueProvider.Invoke();
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
                object oldValue = _fieldData.Get(property);
                if (!object.Equals(oldValue, value))
                {
                    _fieldData.Set(property, value);
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
            object oldValue = _fieldData.Get(property);
            if (!object.Equals(oldValue, value))
            {
                _fieldData.Set(property, value);
                OnPropertyChanged(property.PropertyName);
                OnValueChanged(property.PropertyName, value, oldValue);
                if (property is IRefIdProperty)
                {
                    _fieldData.Set(property.RefEntityProperty, null);
                    OnPropertyChanged(property.RefEntityProperty.PropertyName);
                }
                if (property is IRefEntityProperty)
                {
                    _fieldData.Set(property.RefIdProperty, (value as Entity).GetId());
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

        public void MarkNew()
        {
            State = TrackableState.New;
        }

        public void MarkModified()
        {
            State = TrackableState.Modified;
        }

        public void MarkDeleted()
        {
            State = TrackableState.Deleted;
        }

        public void ResetState()
        {
            State = TrackableState.Unchanged;
        }
    }
}
