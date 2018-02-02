using System.Threading.Tasks;
using Abp.Application.Services;
using Pfizer.QueueSystem.Authorization.Accounts.Dto;

namespace Pfizer.QueueSystem.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
