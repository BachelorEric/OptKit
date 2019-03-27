using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using OptKit.Reflection;
using System.Reflection;
using OptKit.ComponentModel.DataAnnotations;
using OptKit.Domain;

namespace OptKit
{
    public class P<T> where T : Entity
    {
        public static IValueProperty<V> Register<V>(Expression<Func<T, V>> propertyExpr)
        {
            Check.NotNull(propertyExpr, nameof(propertyExpr));
            var propertyName = Reflect<T>.GetProperty(propertyExpr).Name;
            var property = new ValueProperty<V>();
            Set(property, propertyName, typeof(V), typeof(T));
            DomainManager.RegisterProperty(property);
            return property;
        }

        public static IValueProperty Register(string propertyName, Type propertyType, Type declareType)
        {
            Check.NotNullOrEmpty(propertyName, nameof(propertyName));
            Check.NotNull(propertyType, nameof(propertyType));
            Check.NotNull(declareType, nameof(declareType));
            var property = new ValueProperty();
            Set(property, propertyName, propertyType, declareType);
            DomainManager.RegisterProperty(property);
            return property;
        }

        static void Set(Property property, string propertyName, Type propertyType, Type declareType)
        {
            property.OwnerType = typeof(T);
            property.DeclareType = declareType;
            property.PropertyName = propertyName;
            property.PropertyType = propertyType;
        }

        public static ICaculateProperty<V> RegisterCaculate<V>(Expression<Func<T, V>> propertyExpr, Func<T, V> provider, params IProperty[] dependencies)
        {
            Check.NotNull(propertyExpr, nameof(propertyExpr));
            Check.NotNull(provider, nameof(provider));
            var propertyName = Reflect<T>.GetProperty(propertyExpr).Name;
            var property = new CaculateProperty<V>();
            Set(property, propertyName, typeof(V), typeof(T));
            property.ValueProvider = e => provider(e as T);
            return property;
        }

        public static ICaculateProperty RegisterCaculate(string propertyName, Type propertyType, Type declareType, Func<T, object> provider, params IProperty[] dependencies)
        {
            Check.NotNullOrEmpty(propertyName, nameof(propertyName));
            Check.NotNull(declareType, nameof(declareType));
            Check.NotNull(propertyType, nameof(propertyType));
            Check.NotNull(provider, nameof(provider));
            var property = new CaculateProperty();
            Set(property, propertyName, propertyType, declareType);
            property.ValueProvider = e => provider(e as T);
            DomainManager.RegisterProperty(property);
            return property;
        }

        public static ICaculateProperty<V> RegisterCaculate<V>(Expression<Func<T, V>> propertyExpr, string expression, params IProperty[] dependencies)
        {
            Check.NotNull(propertyExpr, nameof(propertyExpr));
            Check.NotNullOrEmpty(expression, nameof(expression));
            var propertyName = Reflect<T>.GetProperty(propertyExpr).Name;
            var property = new CaculateProperty<V>();
            Set(property, propertyName, typeof(V), typeof(T));
            property.Expression = expression;
            return property;
        }

        public static ICaculateProperty RegisterCaculate(string propertyName, Type propertyType, Type declareType, string expression, params IProperty[] dependencies)
        {
            Check.NotNullOrEmpty(propertyName, nameof(propertyName));
            Check.NotNull(propertyType, nameof(propertyType));
            Check.NotNull(declareType, nameof(declareType));
            Check.NotNullOrEmpty(expression, nameof(expression));
            var property = new CaculateProperty();
            Set(property, propertyName, propertyType, declareType);
            property.Expression = expression;
            return property;
        }

        public static IViewProperty<V> RegisterView<V>(Expression<Func<T, V>> propertyExpr, Expression<Func<T, object>> propertyView)
        {
            Check.NotNull(propertyExpr, nameof(propertyExpr));
            Check.NotNull(propertyView, nameof(propertyView));
            var propertyName = Reflect<T>.GetProperty(propertyExpr).Name;
            var viewPath = Reflect<T>.GetPropertyPath(propertyView);
            var property = new ViewProperty<V>();
            Set(property, propertyName, typeof(V), typeof(T));
            property.ViewPath = viewPath;
            return property;
        }

        public static IViewProperty RegisterView(string propertyName, Type propertyType, Type declareType, string viewPath)
        {
            Check.NotNullOrEmpty(propertyName, nameof(propertyName));
            Check.NotNull(propertyType, nameof(propertyType));
            Check.NotNull(declareType, nameof(declareType));
            Check.NotNullOrEmpty(viewPath, nameof(viewPath));
            var property = new ViewProperty();
            Set(property, propertyName, propertyType, declareType);
            property.ViewPath = viewPath;
            DomainManager.RegisterProperty(property);
            return property;
        }

        /// <summary>
        /// 声明一个引用 Id 属性
        /// </summary>
        /// <param name="propertyExpr">指向相应 CLR 的表达式。</param>
        /// <param name="referenceType">引用的类型</param>
        /// <returns></returns>
        public static IRefIdProperty<TKey?> RegisterRefId<TKey>(Expression<Func<T, TKey?>> propertyExpr, ReferenceType referenceType)
            where TKey : struct
        {
            return null;
        }

        /// <summary>
        /// 声明一个引用 Id 属性
        /// </summary>
        /// <param name="propertyExpr">指向相应 CLR 的表达式。</param>
        /// <param name="referenceType">引用的类型</param>
        /// <returns></returns>
        public static IRefIdProperty<TKey> RegisterRefId<TKey>(Expression<Func<T, TKey>> propertyExpr, ReferenceType referenceType)
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
        /// <param name="propertyExpr">指向引用实体属性的表达式。</param>
        /// <param name="refIdProperty">对应的引用 Id 属性，将为其建立关联。</param>
        /// <returns></returns>
        public static IRefEntityProperty<TRefEntity> RegisterRef<TRefEntity>(Expression<Func<T, TRefEntity>> propertyExpr, IRefIdProperty refIdProperty)
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
        /// <param name="propertyExpr">The property exp.</param>
        /// <returns></returns>
        public static IListProperty<TEntityList> RegisterList<TEntityList>(Expression<Func<T, TEntityList>> propertyExpr)
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
