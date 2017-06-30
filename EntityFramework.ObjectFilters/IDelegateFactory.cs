using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EntityFramework.ObjectFilters
{
    public delegate object LateBoundPropertyGet(object target);

    public interface IDelegateFactory
    {
        LateBoundPropertyGet CreateGet(PropertyInfo property);

    }


    public class DelegateFactory : IDelegateFactory
    {
        public LateBoundPropertyGet CreateGet(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            ParameterExpression instanceParameter = Expression.Parameter(typeof(object), "target");

            MemberExpression member = Expression.Property(Expression.Convert(instanceParameter, property.DeclaringType), property);

            Expression<LateBoundPropertyGet> lambda = Expression.Lambda<LateBoundPropertyGet>(Expression.Convert(member, typeof(object)), instanceParameter);

            return lambda.Compile();
        }

        //public LateBoundFieldGet CreateGet(FieldInfo field)
        //{
        //    if (field == null)
        //        throw new ArgumentNullException(nameof(field));

        //    ParameterExpression instanceParameter = Expression.Parameter(typeof(object), "target");

        //    MemberExpression member = Expression.Field(Expression.Convert(instanceParameter, field.DeclaringType), field);

        //    Expression<LateBoundFieldGet> lambda = Expression.Lambda<LateBoundFieldGet>(
        //        Expression.Convert(member, typeof(object)),
        //        instanceParameter
        //    );

        //    return lambda.Compile();
        //}
    }
}