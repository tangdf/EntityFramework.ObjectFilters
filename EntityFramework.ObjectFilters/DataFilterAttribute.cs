using System;

namespace EntityFramework.ObjectFilters
{
    /// <summary>
    /// 对字段条件过滤的标记。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class DataFilterAttribute : Attribute
    {
        /// <summary>
        /// 初始化<see cref="DataFilterAttribute"/>。
        /// </summary>
        /// <param name="comparisonType">字段位比较的类型。</param>
        public DataFilterAttribute(ComparisonType comparisonType)
        {
            ComparisonType = comparisonType;
        }

        /// <summary>
        /// 初始化<see cref="DataFilterAttribute"/>。
        /// </summary>
        public DataFilterAttribute() : this(ComparisonType.Equal)
        {
        }


        //public DataFilterAttribute(string methodName)
        //{
        //    if (methodName == null)
        //        throw new ArgumentNullException(nameof(methodName));

        //    this.MethodName = methodName;
        //}

        /// <summary>
        /// 获取两个字段位比较的类型。
        /// </summary>
        public ComparisonType ComparisonType { get; private set; }

        /// <summary>
        /// 获取或设置条件过滤对应的属性名。
        /// </summary>
        public string PropertyName { get; set; }


        ///// <summary>
        ///// 产生表达式的方法。
        ///// </summary>
        //public string MethodName { get; set; }
    }

    /// <summary>
    /// 对字段条件过滤的标记。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class IgnoreFilterAttribute : Attribute
    {
    }
}