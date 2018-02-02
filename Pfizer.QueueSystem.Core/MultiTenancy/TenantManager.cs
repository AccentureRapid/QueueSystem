using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Pfizer.QueueSystem.Authorization.Users;
using Pfizer.QueueSystem.Editions;

namespace Pfizer.QueueSystem.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore
            ) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore
            )
        {
        }
    }
}