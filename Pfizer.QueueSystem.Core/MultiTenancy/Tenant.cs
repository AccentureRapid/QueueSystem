using Abp.MultiTenancy;
using Pfizer.QueueSystem.Authorization.Users;

namespace Pfizer.QueueSystem.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {
            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}