using OptKit.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptKit
{
    /// <summary>
    /// 属性声明
    /// </summary>
    public interface IProperty
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 属性值类型
        /// </summary>
        Type PropertyType { get; }
        /// <summary>
        /// 属性所有者类型
        /// </summary>
        Type OwnerType { get; }
        /// <summary>
        /// 属性声明类型
        /// </summary>
        Type DeclareType { get; }
        /// <summary>
        /// 编译索引
        /// </summary>
        int CompiledIndex { get; }
        /// <summary>
        /// 特性集合
        /// </summary>
        Attribute[] Attributes { get; }
    }

    /// <summary>
    /// 属性声明
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProperty<T> : IProperty { }
    /// <summary>
    /// 数据属性声明
    /// </summary>
    public interface IDataProperty : IProperty { }

    /// <summary>
    /// 数据属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface IDataProperty<T> : IProperty<T>, IDataProperty { }

    /// <summary>
    /// 计算属性声明
    /// </summary>
    public interface ICaculateProperty : IProperty
    {
        /// <summary>
        /// 表达式
        /// </summary>
        string Expression { get; }
        /// <summary>
        /// 值提供者
        /// </summary>
        Func<DomainObject, object> ValueProvider { get; }
        /// <summary>
        /// 当前属性依赖的属性集合
        /// </summary>
        IProperty[] Dependencies { get; }
    }

    /// <summary>
    /// 计算属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface ICaculateProperty<T> : IProperty<T>, ICaculateProperty { }

    /// <summary>
    /// 引用属性声明
    /// </summary>
    public interface IRefProperty : IProperty
    {
        /// <summary>
        /// 返回对应的引用 Id 属性。
        /// </summary>
        IRefIdProperty RefIdProperty { get; }

        /// <summary>
        /// 返回对应的引用实体属性。
        /// </summary>
        IRefEntityProperty RefEntityProperty { get; }
    }

    /// <summary>
    /// 引用ID属性声明
    /// </summary>
    public interface IRefIdProperty : IRefProperty
    {
        /// <summary>
        /// 实体引用的类型
        /// </summary>
        ReferenceType ReferenceType { get; }
    }

    /// <summary>
    /// 引用ID属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface IRefIdProperty<T> : IProperty<T>, IRefIdProperty { }

    /// <summary>
    /// 引用实体属性声明
    /// </summary>
    public interface IRefEntityProperty : IRefProperty { }

    /// <summary>
    /// 引用实体属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface IRefEntityProperty<T> : IProperty<T>, IRefEntityProperty { }

    /// <summary>
    /// 列表属性声明
    /// </summary>
    public interface IListProperty : IProperty
    {
        /// <summary>
        /// 集合项的类型
        /// </summary>
        Type ItemType { get; }
        /// <summary>
        /// 关联类型
        /// </summary>
        HasManyType HasManyType { get; }
    }

    /// <summary>
    /// 列表属性声明
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IListProperty<T> : IProperty<T>, IListProperty { }
    /// <summary>
    /// 视图属性声明
    /// </summary>
    public interface IViewProperty : IProperty
    {
        /// <summary>
        /// 视图路径
        /// </summary>
        string ViewPath { get; }
    }

    /// <summary>
    /// 视图属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface IViewProperty<T> : IProperty<T>, IViewProperty { }
}
