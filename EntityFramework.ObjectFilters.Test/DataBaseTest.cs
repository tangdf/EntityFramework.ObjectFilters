using System.Linq;
using NUnit.Framework;

namespace EntityFramework.ObjectFilters.Test
{
    [TestFixture]
    public class DataBaseTest : TestBase
    {
        private TestContext _testContext;


        [OneTimeSetUp]
        public void SetUp()
        {
            this._testContext = new TestContext();

            var dbSet = _testContext.Set<Item>();

            this._testContext.Database.ExecuteSqlCommand("delete Item");
            //dbSet.RemoveRange(dbSet);
            dbSet.AddRange(Item.All);

            _testContext.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (this._testContext != null)
                this._testContext.Dispose();
        }


        protected override IQueryable<Item> Items
        {
            get { return _testContext.Set<Item>(); }
        }
    }
}