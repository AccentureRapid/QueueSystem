using System.Linq;
using Pfizer.QueueSystem.EntityFramework;
using Pfizer.QueueSystem.MultiTenancy;

namespace Pfizer.QueueSystem.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly QueueSystemDbContext _context;

        public DefaultTenantCreator(QueueSystemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
