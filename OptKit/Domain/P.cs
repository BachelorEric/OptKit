using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace OptKit.Domain
{
    public class P<T> where T : Entity
    {
        public static IValueProperty<V> Register<V>(Expression<Func<T, V>> propertyExp)
        {
            return null;
        }
        public static ICaculateProperty<V> RegisterCaculate<V>(Expression<Func<T, V>> propertyExp, Func<T, V> provider, params IProperty[] dependencies)
        {
            return null;
        }

        public static IValueProperty<V> Register<V>(string propertyName, Type declareType)
        {
            return null;
        }

        public static ICaculateProperty<V> RegisterCaculate<V>(string property, Type declareType, Func<T, V> provider, params IProperty[] dependencies)
        {
            return null;
        }

        /// <summary>
        /// 声明一个引用 Id 属性
        /// </summary>
        /// <param name="propertyExp">指向相应 CLR 的表达式。</param>
        /// <param name="referenceType">引用的类型</param>
        /// <returns></returns>
        public static IRefIdProperty<TKey?> RegisterRefId<TKey>(Expression<Func<T, TKey?>> propertyExp, ReferenceType referenceType)
            where TKey : struct
        {
            return null;
        }

        /// <summary>
        /// 声明一个引用 Id 属性
        /// </summary>
        /// <param name="propertyExp">指向相应 CLR 的表达式。</param>
        /// <param name="referenceType">引用的类型</param>
        /// <returns></returns>
        public static IRefIdProperty<TKey> RegisterRefId<TKey>(Expression<Func<T, TKey>> propertyExp, ReferenceType referenceType)
        {
            return null;
        }

        public static IRefIdProperty RegisterRefId<TKey>(string propertyName, Type declareType, ReferenceType referenceType, bool isKeyNullable = true)
            where TKey : struct
        {
            return null;
        }

        /// <summary>
        /// 声明一个引用实体属性。
        /// </summary>
        /// <typeparam name="TRefEntity"></typeparam>
        /// <param name="propertyExp">指向引用实体属性的表达式。</param>
        /// <param name="refIdProperty">对应的引用 Id 属性，将为其建立关联。</param>
        /// <returns></returns>
        public static IRefEntityProperty<TRefEntity> RegisterRef<TRefEntity>(Expression<Func<T, TRefEntity>> propertyExp, IRefIdProperty refIdProperty)
            where TRefEntity : Entity
        {
            return null;
        }
        public static IRefEntityProperty<TRefEntity> RegisterRef<TRefEntity>(string propertyName, Type declareType, IRefIdProperty refIdProperty)
           where TRefEntity : Entity
        {
            return null;
        }

        /// <summary>
        /// 注册一个列表属性
        /// </summary>
        /// <typeparam name="TEntityList">The type of the entity list.</typeparam>
        /// <param name="propertyExp">The property exp.</param>
        /// <returns></returns>
        public static IListProperty<TEntityList> RegisterList<TEntityList>(Expression<Func<T, TEntityList>> propertyExp)
            where TEntityList : IEntityList
        {
            return null;
        }

        public static IListProperty<TEntityList> RegisterList<TEntityList>(string propertyName, Type declareType)
            where TEntityList : IEntityList
        {
            return null;
        }
    }
}
