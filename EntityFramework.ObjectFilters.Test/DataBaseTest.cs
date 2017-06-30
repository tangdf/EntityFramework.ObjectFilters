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
    public class DataBaseTest: TestBase
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
