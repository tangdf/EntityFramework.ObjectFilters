using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace EntityFramework.ObjectFilters.Test
{
    public abstract class TestBase
    {
        protected abstract IQueryable<Item> Items { get; }

        [Test]
        public void Demo()
        {
            var filter = new Filter {
                Int = 1,
                Array = new int[0],
                List = new List<int>()
            };

            Expression<Func<Item, bool>> expression = filter.ToExpression<Item>();

            var result = Items.Where(expression).ToList();
            //var i = filter.Int;
            //expression = t => t.Int == i;
            //  result = Items.Where(expression).ToList();
            //return;
            Assert.AreEqual(1, result.Count);

            var item = result[0];

            Assert.AreEqual(1, item.Int);


            filter = new Filter {
                String = "abcdefg",
                Array = new int[0],
                List = new List<int>()
            };

            expression = filter.ToExpression<Item>();

            result = Items.Where(expression).ToList();

            Assert.AreEqual(1, result.Count);

            item = result[0];

            Assert.AreEqual(1, item.Int);
        }


        [Test]
        public void Array_Test_In()
        {
            //测试空数组，返回所有数据
            var filter = new Filter {
                Array = new int[0],
                List = new List<int>()
            };

            Expression<Func<Item, bool>> expression = filter.ToExpression<Item>();
            var result = Items.Where(expression).ToList();


            Assert.AreEqual(4, result.Count);

            // 
            filter.Array = new int[] {
                2
            };

            expression = filter.ToExpression<Item>();
            result = Items.Where(expression).ToList();

            Assert.AreEqual(1, result.Count);
            var item = result[0];

            Assert.AreEqual(2, item.Int);


            filter.Array = new int[] {
                2,
                3
            };

            expression = filter.ToExpression<Item>();
            result = Items.Where(expression).ToList();

            Assert.AreEqual(2, result.Count);
        }


        [Test]
        public void List_Test_In()
        {
            //测试空数组，返回所有数据
            var filter = new Filter {
                Array = new int[0],
                List = new List<int>()
            };

            Expression<Func<Item, bool>> expression = filter.ToExpression<Item>();
            var result = Items.Where(expression).ToList();

            Assert.AreEqual(4, result.Count);

            // 
            filter.List.Add(2);

            expression = filter.ToExpression<Item>();
            result = Items.Where(expression).ToList();

            Assert.AreEqual(1, result.Count);
            var item = result[0];

            Assert.AreEqual(2, item.Int);


            filter.List.Add(3);

            expression = filter.ToExpression<Item>();
            result = Items.Where(expression).ToList();

            Assert.AreEqual(2, result.Count);
        }


        [Test]
        public void Range_Test()
        {
            //测试空数组，返回所有数据
            var filter = new Filter {
                StartDateTime = new DateTime(2010, 2, 2)
            };

            Expression<Func<Item, bool>> expression = filter.ToExpression<Item>();
            var result = Items.Where(expression).ToList();

            Assert.AreEqual(3, result.Count);

            filter.EndDateTime = new DateTime(2010, 2, 2);

            expression = filter.ToExpression<Item>();
            result = Items.Where(expression).ToList();

            Assert.AreEqual(1, result.Count);

            var item = result[0];

            Assert.AreEqual(2, item.Int);
        }


        [Test]
        public void LikeTest()
        {
            var filter = new Filter {
                Contains = "de"
            };

            Expression<Func<Item, bool>> expression = filter.ToExpression<Item>();

            var result = Items.Where(expression).ToList();

            Assert.AreEqual(1, result.Count);

            var item = result[0];

            Assert.AreEqual(1, item.Int);


            filter = new Filter {
                StartsWith = "hij"
            };

            expression = filter.ToExpression<Item>();

            result = Items.Where(expression).ToList();

            Assert.AreEqual(1, result.Count);

            item = result[0];

            Assert.AreEqual(2, item.Int);


            filter = new Filter {
                EndsWith = "xyz"
            };

            expression = filter.ToExpression<Item>();

            result = Items.Where(expression).ToList();

            Assert.AreEqual(1, result.Count);

            item = result[0];

            Assert.AreEqual(4, item.Int);
        }
    }
}