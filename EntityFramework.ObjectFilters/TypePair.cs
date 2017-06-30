using System;

namespace EntityFramework.ObjectFilters
{
    internal class TypePair : IEquatable<TypePair>
    {
        public TypePair(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;

            _hashcode = unchecked((SourceType.GetHashCode() * 397) ^ TargetType.GetHashCode());
        }

        private readonly int _hashcode;

        public Type SourceType { get; }

        public Type TargetType { get; }

        public bool Equals(TypePair other)
        {
            if (other == null)
                return false;

            return other.SourceType == SourceType && other.TargetType == TargetType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (obj is TypePair == false)
                return false;
            return Equals((TypePair) obj);
        }

        public override int GetHashCode()
        {
            return _hashcode;
        }
    }
}