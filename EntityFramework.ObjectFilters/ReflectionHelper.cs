using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EntityFramework.ObjectFilters
{
    internal static class ReflectionHelper
    {
        private static IEnumerable<PropertyInfo> GetDeclaredProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }


        internal static List<PropertyInfo> GetAllPublicReadableProperties(this Type type)
        {
            return GetAllPublicProperties(type).Where(p => p.CanRead).ToList();
        }


        private static IEnumerable<PropertyInfo> GetAllPublicProperties(this Type type)
        {
            return GetDeclaredProperties(type).Where(p => p.GetIndexParameters().Length == 0);
        }


        public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, out Type implementingType)
        {
            if (!genericInterfaceDefinition.IsInterface || !genericInterfaceDefinition.IsGenericTypeDefinition) {
                throw new ArgumentNullException(string.Format("'{0}' is not a generic interface definition.", genericInterfaceDefinition));
            }

            if (type.IsInterface) {
                if (type.IsGenericType) {
                    Type interfaceDefinition = type.GetGenericTypeDefinition();

                    if (genericInterfaceDefinition == interfaceDefinition) {
                        implementingType = type;
                        return true;
                    }
                }
            }

            foreach (Type i in type.GetInterfaces()) {
                if (i.IsGenericType) {
                    Type interfaceDefinition = i.GetGenericTypeDefinition();

                    if (genericInterfaceDefinition == interfaceDefinition) {
                        implementingType = i;
                        return true;
                    }
                }
            }

            implementingType = null;
            return false;
        }
    }
}