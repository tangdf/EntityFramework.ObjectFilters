using System.Data.Entity;

namespace EntityFramework.ObjectFilters.Test
{
    public partial class TestContext : DbContext
    {
        public TestContext() : base("name=Default")
        {
        }

        public virtual DbSet<Item> Item { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}