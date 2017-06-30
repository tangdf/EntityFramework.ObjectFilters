using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.ObjectFilters.Test
{
    [Table("Item")]
    public class Item
    {
        public int ItemId { get; set; }

        [StringLength(50)]
        public string String { get; set; }

        public int? Int { get; set; }

        public DateTime DateTime { get; set; }


        public static List<Item> All = new List<Item> {
            new Item {
                String = "abcdefg",
                Int = 1,
                DateTime = new DateTime(2010, 1, 1)
            },
            new Item {
                String = "hijlmn",
                Int = 2,
                DateTime = new DateTime(2010, 2, 2)
            },
            new Item {
                String = "opqrst",
                Int = 3,
                DateTime = new DateTime(2010, 3, 3)
            },
            new Item {
                String = "uvwxyz",
                Int = 4,
                DateTime = new DateTime(2010, 4, 4)
            },
        };
    }
}