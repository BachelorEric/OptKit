using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace OptKit.Reflection
{
    /// <summary>
    /// Provides strong-typed reflection of the <typeparamref name="TTarget"/> 
    /// type.
    /// </summary>
    /// <typeparam name="TTarget">Type to reflect.</typeparam>
    public static class Reflect<TTarget>
    {
        /// <summary>
        /// Gets the method represented by the lambda expression.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static MethodInfo GetMethod(Expression<Action<TTarget>> method)
        {
            return GetMethodInfo(method);
        }

        /// <summary>
        /// Gets the method represented by the lambda expression.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public static MethodInfo GetMethod<T1>(Expression<Action<TTarget, T1>> method)
        {
            return GetMethodInfo(method);
        }

        /// <summary>
        /// Gets the method represented by the lambda expression.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <paramref name="method"/> is null.</exception>
        /// <exception cref="ArgumentException">The <paramref name="method"/> is not a lambda expression or it does not represent a method invocation.</exception>
        public static MethodInfo GetMethod<T1, T2>(Expression<Action<TTarget, T1, T2>> method)
        {
            return GetMethodInfo(method);
        }

        /// <summary>
        /// Gets the method represented by the lambda expression.
        /// </summary>
        /// <exception cref="ArgumentNullException">The <paramref name="method"/> is null.</exception>
        /// <exception cref="ArgumentException">The <paramref name="method"/> is not a lambda expression or it does not represent a method invocation.</exception>
        public static MethodInfo GetMethod<T1, T2, T3>(Expression<Action<TTarget, T1, T2, T3>> method)
        {
            return GetMethodInfo(method);
        }

        private static MethodInfo GetMethodInfo(Expression method)
        {
            Check.NotNull(method, nameof(method));

            var lambda = method as LambdaExpression;
            if (lambda == null) throw new ArgumentException("[{0}] is not a lambda expression".FormatArgs(method), nameof(method));
            if (lambda.Body.NodeType != ExpressionType.Call) throw new ArgumentException("Not a method call", "method");

            return ((MethodCallExpression)lambda.Body).Method;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Member is not a property</exception>
        public static PropertyInfo GetProperty(Expression<Func<TTarget, object>> property)
        {
            var info = GetMemberInfo(property) as PropertyInfo;
            if (info == null) throw new ArgumentException("Member[{0}] is not a property".FormatArgs(property), nameof(property));

            return info;
        }

        /// <summary>
        /// Gets the property represented by the lambda expression.
        /// </summary>
        /// <typeparam name="P">Type assigned to the property</typeparam>
        /// <param name="property">Property Expression</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Member is not a property</exception>
        public static PropertyInfo GetProperty<P>(Expression<Func<TTarget, P>> property)
        {
            var info = GetMemberInfo(property) as PropertyInfo;
            if (info == null) throw new ArgumentException("Member[{0}] is not a property".FormatArgs(property), nameof(property));

            return info;
        }

        /// <summary>
        /// Lambda表达式为 p.Group.Name 时，返回 Group.Name
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string GetPropertyPath<P>(Expression<Func<TTarget, P>> exp)
        {
            Check.NotNull(exp, nameof(exp));
            var visitor = new PropertyVisitor();
            visitor.Visit(exp);
            return visitor.Path;
        }

        /// <summary>
        /// Gets the field represented by the lambda expression.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Member is not a field</exception>
        public static FieldInfo GetField(Expression<Func<TTarget, object>> field)
        {
            var info = GetMemberInfo(field) as FieldInfo;
            if (info == null) throw new ArgumentException("Member[{0}] is not a field".FormatArgs(field), nameof(field));

            return info;
        }

        private static MemberInfo GetMemberInfo(LambdaExpression lambda)
        {
            Check.NotNull(lambda, nameof(lambda));
            MemberExpression memberExpr = null;

            // The Func<TTarget, object> we use returns an object, so first statement can be either 
            // a cast (if the field/property does not return an object) or the direct member access.
            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                // The cast is an unary expression, where the operand is the 
                // actual member access expression.
                memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null) throw new ArgumentException("[{0}] is not a member access".FormatArgs(lambda), nameof(lambda));

            return memberExpr.Member;
        }
    }

    /// <summary>
    /// 属性路径访问器。Lambda表达式为 p.Group.Name 时，<see cref="PropertyVisitor.Path"/> 为 Group.Name
    /// </summary>
    class PropertyVisitor : ExpressionVisitor
    {
        /// <summary>
        /// 属性路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 访问成员
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            if (Path.IsNotEmpty())
                Path = "." + Path;
            Path = node.Member.Name + Path;
            return base.VisitMember(node);
        }
    }
}