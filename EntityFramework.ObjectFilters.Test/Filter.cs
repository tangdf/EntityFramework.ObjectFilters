using System;
using System.Collections.Generic;

namespace EntityFramework.ObjectFilters.Test
{
    public class Filter
    {
        public string String { get; set; }

        public int? Int { get; set; }

        [DataFilter(PropertyName = nameof(Item.Int))]
        public int[] Array { get; set; }

        [DataFilter(PropertyName = nameof(Item.Int))]
        public List<int> List { get; set; }

        [DataFilter(ComparisonType.GreaterThanOrEqual, PropertyName = nameof(Item.DateTime))]
        public DateTime? StartDateTime { get; set; }

        [DataFilter(ComparisonType.LessThanOrEqual, PropertyName = nameof(Item.DateTime))]
        public DateTime? EndDateTime { get; set; }

        [IgnoreFilter]
        public int? IgnoreInt { get; set; }


        [DataFilter(ComparisonType.Contains, PropertyName = nameof(Item.String))]
        public string Contains { get; set; }

        [DataFilter(ComparisonType.StartsWith, PropertyName = nameof(Item.String))]
        public string StartsWith { get; set; }


        [DataFilter(ComparisonType.EndsWith, PropertyName = nameof(Item.String))]
        public string EndsWith { get; set; }
    }
}