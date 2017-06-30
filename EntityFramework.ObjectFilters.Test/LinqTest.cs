using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MapExpression.UnitTest
{
    [TestFixture]
    public class LinqTest:TestBase
    {

        protected override IQueryable<Item> Items
        {
            get { return Item.All.AsQueryable(); }
        }
    }
}
