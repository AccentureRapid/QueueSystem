using System.Threading.Tasks;
using Abp.Application.Services;
using Pfizer.QueueSystem.Sessions.Dto;

namespace Pfizer.QueueSystem.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
