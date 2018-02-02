using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using Pfizer.QueueSystem.EntityFramework;

namespace Pfizer.QueueSystem
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(QueueSystemCoreModule))]
    public class QueueSystemDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<QueueSystemDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
