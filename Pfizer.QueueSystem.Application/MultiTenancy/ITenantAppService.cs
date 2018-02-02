using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Pfizer.QueueSystem.MultiTenancy.Dto;

namespace Pfizer.QueueSystem.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
