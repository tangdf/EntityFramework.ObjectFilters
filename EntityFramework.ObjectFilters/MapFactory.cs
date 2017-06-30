using System;
using System.Collections.Concurrent;

namespace EntityFramework.ObjectFilters
{
    internal static class MapFactory
    {
        private static readonly ConcurrentDictionary<TypePair, TypeMap> s_typeMapCache = new ConcurrentDictionary<TypePair, TypeMap>();


        public static TypeMap GetMap(TypePair typePair)
        {
            if (typePair == null)
                throw new ArgumentNullException(nameof(typePair));

            return s_typeMapCache.GetOrAdd(typePair, CreateMap);
        }


        private static TypeMap CreateMap(TypePair typePair)
        {
            var typeMap = new TypeMap(typePair);

            typeMap.Initialize();

            return typeMap;
        }
    }
}