namespace LabaManage.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<LabaManage.Data.Concrete.EFDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LabaManage.Data.Concrete.EFDbContext context)
        {
            base.Seed(context);
        }
    }
}
