namespace EntityFramework.ObjectFilters
{
    /// <summary>
    /// 用于过滤条件中两个对象比较类型。
    /// </summary>
    public enum ComparisonType
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equal,

        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual,

        /// <summary>
        /// 大于
        /// </summary>
        Greaterthan,

        /// <summary>
        /// 小于
        /// </summary>
        LessThan,

        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// 包含（%like%）
        /// </summary>
        Contains,

        /// <summary>
        /// 包含（like%）   
        /// </summary>
        StartsWith,

        /// <summary>
        /// 包含（%like）   
        /// </summary>
        EndsWith
    }
}