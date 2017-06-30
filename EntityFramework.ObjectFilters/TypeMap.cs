using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace EntityFramework.ObjectFilters
{
    [DebuggerDisplay("{SourceType.Name} -> {TargetType.Name}")]
    internal class TypeMap
    {
        private readonly IList<PropertyMap> _maps = new List<PropertyMap>();

        private static readonly IDelegateFactory s_delegateFactory = new DelegateFactory();

        public TypeMap(TypePair typePair) : this(typePair.SourceType, typePair.TargetType)
        {
        }

        public TypeMap(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
        }

        public Type SourceType { get; private set; }


        public Type TargetType { get; private set; }

        public IEnumerable<PropertyMap> PropertyMaps
        {
            get { return _maps; }
        }


        public void Initialize()
        {
            //这里与 MVC 5 保持一致，只获取所有的属性。
            var sourceProperties = SourceType.GetAllPublicReadableProperties();

            var targetProperties = TargetType.GetAllPublicReadableProperties();


            foreach (PropertyInfo sourcePropertyInfo in sourceProperties) {
                if (sourcePropertyInfo.IsDefined(typeof(IgnoreFilterAttribute), true))
                    continue;

                CreatePropertyMap(sourcePropertyInfo, targetProperties);
            }
        }

        private void CreatePropertyMap(PropertyInfo sourceProperty, List<PropertyInfo> targetProperties)
        {
            DataFilterAttribute dataFilterAttribute = sourceProperty.GetCustomAttribute<DataFilterAttribute>();

            var targetPropertyName = dataFilterAttribute?.PropertyName;
            if (string.IsNullOrEmpty(targetPropertyName))
                targetPropertyName = sourceProperty.Name;

            var targetProperty = FindTargetProperty(targetProperties, targetPropertyName);

            if (targetProperty == null)
                throw new InvalidOperationException(string.Format("在类型{0}上未找到{1}属性。", TargetType.FullName, targetPropertyName));

            PropertyMap propertyMap = CreateMap(sourceProperty, targetProperty, dataFilterAttribute);

            propertyMap.ComparisonType = dataFilterAttribute?.ComparisonType ?? ComparisonType.Equal;


            _maps.Add(propertyMap);
        }

        private static PropertyMap CreateMap(PropertyInfo sourceProperty, PropertyInfo targetProperty, DataFilterAttribute dataFilterAttribute)
        {
            bool isEnumerable;

            var sourceType = GetSourceRealType(sourceProperty.PropertyType, out isEnumerable);
            var targetType = targetProperty.PropertyType;

            targetType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (sourceType != targetType)
                throw new InvalidOperationException("源字段与目标字段的类型不一致。");

            if (isEnumerable && dataFilterAttribute != null && dataFilterAttribute.ComparisonType != ComparisonType.Equal)
                throw new InvalidOperationException("In 语句只能使用相等操作符。");

            Type propertyType = targetType;

            PropertyMap propertyMap = new PropertyMap();

            propertyMap.SourceProperty = sourceProperty;
            propertyMap.TargetProperty = targetProperty;
            propertyMap.IsEnumerable = isEnumerable;
            propertyMap.PropertyType = propertyType;
            propertyMap.SourceGetter = s_delegateFactory.CreateGet(sourceProperty);
            propertyMap.IsNullable = targetProperty.PropertyType != targetType;
            return propertyMap;
        }


        private static Type GetSourceRealType(Type sourceType, out bool isEnumerable)
        {
            if (sourceType == typeof(string)) {
                isEnumerable = false;
                return sourceType;
            }

            sourceType = Nullable.GetUnderlyingType(sourceType) ?? sourceType;

            Type implementingType;
            if (ReflectionHelper.ImplementsGenericDefinition(sourceType, typeof(IEnumerable<>), out implementingType)) {
                isEnumerable = true;
                return implementingType.GenericTypeArguments[0];
            }

            if (sourceType.HasElementType) {
                isEnumerable = true;
                return sourceType.GetElementType();
            }
            isEnumerable = false;
            return sourceType;
        }

        private PropertyInfo FindTargetProperty(List<PropertyInfo> targetProperties, string propertyName)
        {
            return targetProperties.FirstOrDefault(item => string.Equals(item.Name, propertyName, StringComparison.OrdinalIgnoreCase));
        }
    }
}