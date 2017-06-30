using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EntityFramework.ObjectFilters
{
    /// <summary>
    /// 生成where表达式。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExpressionBuilder<T> where T : class
    {
        private Type _target;

        private ParameterExpression _parameterExpression;

        private Type _filterType;

        private readonly object _filter;

        private ConstantExpression _constantExpression;

        public ExpressionBuilder(object filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            _filter = filter;
        }


        public Expression<Func<T, bool>> Build()
        {
            _target = typeof(T);
            _filterType = _filter.GetType();
            _constantExpression = Expression.Constant(_filter, _filterType);

            TypeMap typeMap = MapFactory.GetMap(new TypePair(_filterType, _target));

            Expression result = null;

            //_constantExpression = Expression.Constant(_filter, _filterType);
            _parameterExpression = Expression.Parameter(typeof(T), "item");

            foreach (PropertyMap propertyMap in typeMap.PropertyMaps) {
                var propertyValue = propertyMap.SourceGetter.Invoke(_filter);

                if (propertyValue == null)
                    continue;
                Expression propertyExpression;
                if (propertyMap.IsEnumerable) {
                    IEnumerable enumerable = (IEnumerable) propertyValue;

                    // ReSharper disable once PossibleMultipleEnumeration
                    var sourceLength = GetCount(enumerable);

                    if (sourceLength == 0)
                        continue;

                    propertyExpression = BuildIn(propertyMap, enumerable);
                }
                else
                    propertyExpression = Build(propertyMap);


                result = result == null ? propertyExpression : Expression.AndAlso(result, propertyExpression);
            }

            if (result == null)
                return ExpressionHelper.True<T>();

            return Expression.Lambda<Func<T, bool>>(result, _parameterExpression);
        }

        private int GetCount(IEnumerable enumerable)
        {
            ICollection collection = enumerable as ICollection;
            if (collection != null)
                return collection.Count;
            return enumerable.Cast<object>().ToList().Count;
        }

        private Expression BuildIn(PropertyMap propertyMap, IEnumerable value)
        {
            Expression varExpression = Expression.Constant(value);
            Expression memberExpression = Expression.Property(_parameterExpression, propertyMap.TargetProperty);

            if (propertyMap.IsNullable)
                memberExpression = Expression.Convert(memberExpression, propertyMap.PropertyType);

            MethodInfo methodInfo = ExpressionHelper.s_arrayContainsMethod.MakeGenericMethod(propertyMap.PropertyType);

            return Expression.Call(methodInfo, varExpression, memberExpression);
        }


        private Expression Build(PropertyMap propertyMap)
        {
            Expression varExpression = Expression.Property(_constantExpression, propertyMap.SourceProperty);

            //Expression varExpression = Expression.Variable(value);

            Expression memberExpression = Expression.Property(_parameterExpression, propertyMap.TargetProperty);

            varExpression = Expression.Convert(varExpression, propertyMap.PropertyType);

            if (propertyMap.IsNullable)
                memberExpression = Expression.Convert(memberExpression, propertyMap.PropertyType);

            Expression propertyExpression;
            switch (propertyMap.ComparisonType) {
                case ComparisonType.Equal:
                    propertyExpression = Expression.Equal(memberExpression, varExpression);
                    break;

                case ComparisonType.NotEqual:
                    propertyExpression = Expression.NotEqual(memberExpression, varExpression);
                    break;

                case ComparisonType.Greaterthan:
                    propertyExpression = Expression.GreaterThan(memberExpression, varExpression);
                    break;

                case ComparisonType.LessThan:
                    propertyExpression = Expression.LessThan(memberExpression, varExpression);
                    break;

                case ComparisonType.GreaterThanOrEqual:
                    propertyExpression = Expression.GreaterThanOrEqual(memberExpression, varExpression);
                    break;

                case ComparisonType.LessThanOrEqual:
                    //处理时间类型。

                    propertyExpression = Expression.LessThanOrEqual(memberExpression, varExpression);
                    break;

                case ComparisonType.Contains:
                    propertyExpression = Expression.Call(memberExpression, ExpressionHelper.s_containsMethod, varExpression);
                    break;

                case ComparisonType.StartsWith:
                    propertyExpression = Expression.Call(memberExpression, ExpressionHelper.s_startsWithMethod, varExpression);
                    break;

                case ComparisonType.EndsWith:
                    propertyExpression = Expression.Call(memberExpression, ExpressionHelper.s_endsWithMethod, varExpression);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return propertyExpression;
        }

        //public Expression AndAlso(Expression<Func<T, bool>> expr, Expression expression, ParameterExpression parameterExpression)
        //{
        //    var leftVisitor = new ReplaceExpressionVisitor(expr.Parameters[0], parameterExpression);
        //    var left = leftVisitor.Visit(expr.Body);
        //    if (expression == null)
        //        return left;
        //    return Expression.AndAlso(left, expression);
        //}
    }

    //internal class ReplaceExpressionVisitor : ExpressionVisitor
    //{
    //    private readonly Expression _oldValue;
    //    private readonly Expression _newValue;

    //    public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
    //    {
    //        _oldValue = oldValue;
    //        _newValue = newValue;
    //    }

    //    public override Expression Visit(Expression node)
    //    {
    //        if (node == _oldValue)
    //            return _newValue;
    //        return base.Visit(node);
    //    }
    //}
}