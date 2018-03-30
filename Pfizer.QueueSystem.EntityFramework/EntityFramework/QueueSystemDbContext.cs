using System.Data.Common;
using System.Data.Entity;
using System.Threading.Tasks;
using Abp.Zero.EntityFramework;
using Pfizer.QueueSystem.Authorization.Roles;
using Pfizer.QueueSystem.Authorization.Users;
using Pfizer.QueueSystem.Entities;
using Pfizer.QueueSystem.MultiTenancy;

namespace Pfizer.QueueSystem.EntityFramework
{
    public class QueueSystemDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        public DbSet<QueueHistory> QueueHistory { get; set; }
        public DbSet<FastToken> FastToken { get; set; }
        public DbSet<QueueHistoryLog> QueueHistoryLog { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public QueueSystemDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in QueueSystemDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of QueueSystemDbContext since ABP automatically handles it.
         */
        public QueueSystemDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public QueueSystemDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public QueueSystemDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
