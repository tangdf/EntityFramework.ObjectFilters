using System;

namespace EntityFramework.ObjectFilters
{
    /// <summary>
    /// ���ֶ��������˵ı�ǡ�
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class DataFilterAttribute : Attribute
    {
        /// <summary>
        /// ��ʼ��<see cref="DataFilterAttribute"/>��
        /// </summary>
        /// <param name="comparisonType">�ֶ�λ�Ƚϵ����͡�</param>
        public DataFilterAttribute(ComparisonType comparisonType)
        {
            ComparisonType = comparisonType;
        }

        /// <summary>
        /// ��ʼ��<see cref="DataFilterAttribute"/>��
        /// </summary>
        public DataFilterAttribute() : this(ComparisonType.Equal)
        {
        }


        /// <summary>
        /// ��ȡ�����ֶ�λ�Ƚϵ����͡�
        /// </summary>
        public ComparisonType ComparisonType { get; private set; }

        /// <summary>
        /// ��ȡ�������������˶�Ӧ����������
        /// </summary>
        public string PropertyName { get; set; }
     
    }

    /// <summary>
    /// ���ֶ��������˵ı�ǡ�
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class IgnoreFilterAttribute : Attribute
    {
    }
}