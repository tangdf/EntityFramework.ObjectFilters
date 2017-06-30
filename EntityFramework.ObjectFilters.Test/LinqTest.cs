using System.Linq;
using NUnit.Framework;

namespace EntityFramework.ObjectFilters.Test
{
    [TestFixture]
    public class LinqTest : TestBase
    {
        protected override IQueryable<Item> Items
        {
            get { return Item.All.AsQueryable(); }
        }
    }
}