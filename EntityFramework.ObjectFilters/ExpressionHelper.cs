using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EntityFramework.ObjectFilters
{
    public static class ExpressionHelper
    {
        internal static readonly MethodInfo s_containsMethod = typeof(string).GetMethod("Contains", BindingFlags.Public | BindingFlags.Instance);

        internal static readonly MethodInfo s_startsWithMethod = typeof(string).GetMethod("StartsWith", BindingFlags.Public | BindingFlags.Instance, null, new[] {
            typeof(string)
        }, null);

        internal static readonly MethodInfo s_endsWithMethod = typeof(string).GetMethod("EndsWith", BindingFlags.Public | BindingFlags.Instance, null, new[] {
            typeof(string)
        }, null);


        internal static readonly MethodInfo s_arrayContainsMethod = typeof(Enumerable).GetMethods(BindingFlags.Public | BindingFlags.Static).Single(input => input.Name == "Contains" && input.GetParameters().Length == 2);


        internal static Expression<Func<T, bool>> True<T>()
        {
            return item => true;
        }

        /// <summary>
        /// 将过滤对象转换成为表达式树。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> ToExpression<T>(this object filter) where T : class
        {
            if (filter == null)
                return item => true;

            return new ExpressionBuilder<T>(filter).Build();
        }
    }
}