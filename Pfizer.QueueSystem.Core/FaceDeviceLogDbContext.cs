using System.Data.Common;
using System.Data.Entity;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Pfizer.QueueSystem.Authorization.Roles;
using Pfizer.QueueSystem.Authorization.Users;
using Pfizer.QueueSystem.Entities;
using Pfizer.QueueSystem.MultiTenancy;

namespace Pfizer.QueueSystem.EntityFramework
{
    public class FaceDeviceLogDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for your Entities...
        public DbSet<RequestTableForFace> RequestTableForFace { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public FaceDeviceLogDbContext()
            : base("FaceDeviceLog")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in QueueSystemDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of QueueSystemDbContext since ABP automatically handles it.
         */
        public FaceDeviceLogDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public FaceDeviceLogDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public FaceDeviceLogDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
