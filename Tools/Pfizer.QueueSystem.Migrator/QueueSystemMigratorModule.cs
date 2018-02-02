using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Pfizer.QueueSystem.EntityFramework;

namespace Pfizer.QueueSystem.Migrator
{
    [DependsOn(typeof(QueueSystemDataModule))]
    public class QueueSystemMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<QueueSystemDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}