using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using Pfizer.QueueSystem.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace Pfizer.QueueSystem.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<QueueSystem.EntityFramework.QueueSystemDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "QueueSystem";


        }

        protected override void Seed(QueueSystem.EntityFramework.QueueSystemDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
