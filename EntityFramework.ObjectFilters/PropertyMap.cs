using System;
using System.Reflection;

namespace EntityFramework.ObjectFilters
{
    internal class PropertyMap
    {
        //public PropertyMap(PropertyInfo sourceProperty, PropertyInfo targetProperty , ComparisonType comparisonType,bool isEnumerable,Type propertyType)
        //{
        //    SourceProperty = sourceProperty;
        //    TargetProperty = targetProperty;
        //    ComparisonType = comparisonType;
        //    SourceGetter = sourceProperty.ToMemberGetter();
        //    IsEnumerable = isEnumerable;
        //    PropertyType = propertyType;
        //}

        public Type PropertyType { get; set; }

        public bool IsEnumerable { get; set; }

        public bool IsNullable { get; set; }

        public LateBoundPropertyGet SourceGetter { get; set; }


        public ComparisonType ComparisonType { get; set; }


        public PropertyInfo SourceProperty { get; set; }


        public PropertyInfo TargetProperty { get; set; }
    }
}